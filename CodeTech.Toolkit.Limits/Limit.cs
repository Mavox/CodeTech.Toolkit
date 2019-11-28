using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Limits
{
    /// <summary>
    /// An object used to limit the executions of specific lines of code.
    /// </summary>
    /// <remarks>
    /// This is an updated version of Jack Leitch's RateGate from 2010 (http://www.jackleitch.net/2010/10/better-rate-limiting-with-dot-net/).
    /// </remarks>
    public class Limit : ILimit, IAsynchronousLimit, IDisposable
    {
        /// <summary>
        /// Occurrences to allow.
        /// </summary>
        public int Occurrences { get; }

        /// <summary>
        /// Timeunit to allow occurences within.
        /// </summary>
        public TimeSpan TimeUnit { get; }

        /// <summary>
        /// Indicates whether the Limit has been disposed or not.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        private readonly SemaphoreSlim semaphore;
        private readonly ConcurrentQueue<TimeSpan> queue;
        private readonly CancellationTokenSource cancellationTokenSource;

        // Instead of using a timer with an event 
        // we will be using a task that will run on a separate thread until we dispose of the Limit.
        private readonly Task runningTask;

        // We are using an internal stopwatch instead of the original idea of Enviorment.TickCount
        // to avoid the 49,8 day limit at a cost of a slightly more inaccurate timer.
        private readonly Stopwatch stopWatch;

        /// <summary>
        /// Constructs a new limit that allows a maximum number of occurrences within the given timeunit.
        /// </summary>
        /// <param name="occurrences">Occurrences to allow.</param>
        /// <param name="timeUnit">Timeunit to allow occurences within.</param>
        public Limit(int occurrences, TimeSpan timeUnit)
        {
            if (occurrences > 0)
            {
                if (timeUnit > TimeSpan.Zero)
                {
                    Occurrences = occurrences;
                    TimeUnit = timeUnit;
                    semaphore = new SemaphoreSlim(this.Occurrences, this.Occurrences);

                    stopWatch = new Stopwatch();
                    stopWatch.Start();

                    queue = new ConcurrentQueue<TimeSpan>();
                    cancellationTokenSource = new CancellationTokenSource();

                    runningTask = Task.Run(() => StartAsync(cancellationTokenSource.Token));
                }
                else throw new ArgumentOutOfRangeException(nameof(timeUnit), "Timeunit must be a positive timespan");
            }
            else throw new ArgumentOutOfRangeException(nameof(occurrences), "Number of occurrences must be a positive integer");
        }

        private async Task StartAsync(CancellationToken cancellationToken)
        {
            bool isCanceled = false;
            TimeSpan timeUntilNextCheck = TimeSpan.Zero;
            while (!IsDisposed && !isCanceled)
            {
                try
                {
                    await Task.Delay(timeUntilNextCheck, cancellationToken);
                    timeUntilNextCheck = UpdateSemaphore(semaphore, TimeUnit, stopWatch, queue);
                }
                catch (TaskCanceledException)
                {
                    isCanceled = true;
                }
            }
        }

        /// <summary>
        /// Updates the provided semaphore and exitqueue based on the provided timeunit and running watch
        /// </summary>
        /// <param name="semaphore">Semaphore to update</param>
        /// <param name="timeUnit">Timeunit</param>
        /// <param name="watch">Stopwatch that indicates the lifetime of the current limit</param>
        /// <param name="exitQueue">Queue that holds all current exittimes</param>
        /// <returns>Time until the next call to UpdateSemaphore needs to be done</returns>
        protected virtual TimeSpan UpdateSemaphore(SemaphoreSlim semaphore, TimeSpan timeUnit, Stopwatch watch, ConcurrentQueue<TimeSpan> exitQueue)
        {
            TimeSpan exitTime;
            while (exitQueue.TryPeek(out exitTime) && unchecked(exitTime - watch.Elapsed) <= TimeSpan.Zero)
            {
                if (!IsDisposed)
                {
                    semaphore.Release();                    
                }

                exitQueue.TryDequeue(out exitTime);
            }

            TimeSpan timeUntilNextCheck;
            if (exitQueue.TryPeek(out exitTime))
            {
                timeUntilNextCheck = unchecked(exitTime - watch.Elapsed);
            }
            else
            {
                timeUntilNextCheck = timeUnit;
            }

            return timeUntilNextCheck;
        }

        /// <summary>
        /// Blocks the current thread until the Limit deems it appropriate to continue.
        /// </summary>
        /// <returns>True if we entered the internal Semaphore, otherwise false.</returns>
        public bool Wait()
        {
            return ProcessWaitRequest(Timeout.InfiniteTimeSpan, semaphore, TimeUnit, stopWatch, queue);
        }

        /// <summary>
        /// Blocks the current thread until the Limit deems it appropriate to continue or until the timeout is reached
        /// </summary>
        /// <param name="timeout">A timeout that indicates the maximum time you're willing to wait.</param>
        /// <returns>True if we entered the internal Semaphore, otherwise false.</returns>
        public bool Wait(TimeSpan timeout)
        {
            return ProcessWaitRequest(timeout, semaphore, TimeUnit, stopWatch, queue);
        }

        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue.
        /// </summary>
        /// <returns>A task that finishes when it is ok to continue.</returns>
        public Task<bool> WaitAsync()
        {
            return ProcessWaitRequestAsync(Timeout.InfiniteTimeSpan, CancellationToken.None, semaphore, TimeUnit, stopWatch, queue);
        }

        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue or until the timeout is reached.
        /// </summary>
        /// <param name="timeout">A timeout that indicates the maximum time you're willing to wait.</param>
        /// <returns>A task that finishes when it is ok to continue or when the timeout is reached.</returns>
        public Task<bool> WaitAsync(TimeSpan timeout)
        {
            return ProcessWaitRequestAsync(timeout, CancellationToken.None, semaphore, TimeUnit, stopWatch, queue);
        }

        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue while observing a cancellationtoken.
        /// </summary>
        /// <param name="cancellationToken">Cancellationtoken to observe.</param>
        /// <returns>A task that finishes when it is ok to continue.</returns>
        public Task<bool> WaitAsync(CancellationToken cancellationToken)
        {
            return ProcessWaitRequestAsync(Timeout.InfiniteTimeSpan, cancellationToken, semaphore, TimeUnit, stopWatch, queue);
        }

        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue or until the timeout is reached while observing a cancellationtoken.
        /// </summary>
        /// <param name="timeout">A timeout that indicates the maximum time you're willing to wait.</param>
        /// <param name="cancellationToken">Cancellationtoken to observe.</param>
        /// <returns>A task that finishes when it is ok to continue or when the timeout is reached.</returns>
        public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
        {
            return ProcessWaitRequestAsync(timeout, cancellationToken, semaphore, TimeUnit, stopWatch, queue);
        }

        /// <summary>
        /// Handles all calls to the different overloads of WaitAsync.
        /// </summary>
        /// <param name="timeout">Maximum time the caller is willing to wait.</param>
        /// <param name="cancellationToken">Cancellationtoken to observe.</param>
        /// <param name="semaphore">Semaphore to use when blocking.</param>
        /// <param name="timeUnit">Timeunit.</param>
        /// <param name="watch">Stopwatch that indicates the lifetime of the current limit.</param>
        /// <param name="exitQueue">Queue that holds all current exittimes.</param>
        /// <returns>A task that finishes once the semaphore allows it to.</returns>
        protected virtual async Task<bool> ProcessWaitRequestAsync(TimeSpan timeout, CancellationToken cancellationToken, SemaphoreSlim semaphore, TimeSpan timeUnit, Stopwatch watch, ConcurrentQueue<TimeSpan> exitQueue)
        {
            if (!IsDisposed)
            {
                if (timeout > TimeSpan.Zero || timeout == Timeout.InfiniteTimeSpan)
                {
                    // Block until we can enter the semaphore or until the timeout expires.
                    var entered = await semaphore.WaitAsync(timeout, cancellationToken);

                    // If we entered the semaphore, compute the corresponding exit time 
                    // and add it to the queue.
                    if (entered)
                    {
                        var timeToExit = unchecked(watch.Elapsed + timeUnit);
                        exitQueue.Enqueue(timeToExit);
                    }

                    return entered;
                }
                else throw new ArgumentOutOfRangeException(nameof(timeout), "Timeout must be a positive timespan");
            }
            else throw new ObjectDisposedException("Limit has already been disposed");
        }

        /// <summary>
        /// Handles all calls to the different overloads of Wait.
        /// </summary>
        /// <param name="timeout">Maximum time the caller is willing to wait.</param>
        /// <param name="semaphore">Semaphore to use when blocking.</param>
        /// <param name="timeUnit">Timeunit.</param>
        /// <param name="watch">Stopwatch that indicates the lifetime of the current limit.</param>
        /// <param name="exitQueue">Queue that holds all current exittimes.</param>
        /// <returns>True if we entered the semaphore, otherwise false.</returns>
        protected virtual bool ProcessWaitRequest(TimeSpan timeout, SemaphoreSlim semaphore, TimeSpan timeUnit, Stopwatch watch, ConcurrentQueue<TimeSpan> exitQueue)
        {
            if (!IsDisposed)
            {
                if (timeout > TimeSpan.Zero || timeout == Timeout.InfiniteTimeSpan)
                {
                    // Block until we can enter the semaphore or until the timeout expires.
                    var entered = semaphore.Wait(timeout);

                    // If we entered the semaphore, compute the corresponding exit time 
                    // and add it to the queue.
                    if (entered)
                    {
                        var timeToExit = unchecked(watch.Elapsed + timeUnit);
                        exitQueue.Enqueue(timeToExit);
                    }

                    return entered;
                }
                else throw new ArgumentOutOfRangeException(nameof(timeout), "Timeout must be a positive timespan");
            }
            else throw new ObjectDisposedException("Limit has already been disposed");
        }

        /// <summary>
        /// Disposes the limit and all its related resources.
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                semaphore.Dispose();
                stopWatch.Stop();
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }
    }
}
