using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Web.HostedServices
{
    public class BackgroundWorkerService : BackgroundService, IBackgroundWorkerService, IAsynchronousBackgroundWorkerService
    {
        public BackgroundWorkerService()
        {
            tasks = new List<Task>();
            taskRemovalSemaphore = new SemaphoreSlim(1);
        }

        CancellationToken cancellationToken;
        // Used to make internal task removal thread safe
        readonly SemaphoreSlim taskRemovalSemaphore;
        // Contains a collection of all currently running tasks (finished tasks will be removed)
        readonly List<Task> tasks;

        public EventHandler<Exception> OnException { get; set; }

        /// <summary>
        /// Indicates wether the service is running or not
        /// </summary>
        public bool IsStarted { get; private set; }

        /// <summary>
        /// Indicates wether the service is currently processing any work
        /// </summary>
        public bool IsProcessingTasks { get
            {
                return tasks.Any(x => !x.IsCompleted);
            }
        }

        /// <summary>
        /// Runs the provided work on the threadpool
        /// </summary>
        /// <remarks>
        /// If cancellation is requested, no exception will be thrown.
        /// Be sure to watch for cancellation requests on the provided cancellationtoken 
        /// and implement graceful cancellation logic when using this overload.
        /// </remarks>
        /// <param name="work">Work to perform</param>
        public void Run(Action<CancellationToken> work)
        {            
            RunInternal(Task.Run(() => work(cancellationToken)));
        }

        /// <summary>
        /// Runs the provided work asynchronously
        /// </summary>
        /// <remarks>
        /// If cancellation is requested, no exception will be thrown.
        /// Be sure to watch for cancellation requests on the provided cancellationtoken 
        /// and implement graceful cancellation logic when using this overload.
        /// </remarks>
        /// <param name="work">Work to perform</param>
        public void RunAsync(Func<CancellationToken, Task> work)
        {
            RunInternal(work(cancellationToken));
        }

        /// <summary>
        /// Runs the provided work asynchronously
        /// </summary>
        /// <param name="work">Work to perform</param>
        void RunInternal(Task task)
        {
            if (IsStarted)
            {
                tasks.Add(task);
                task.ContinueWith(async completedTask =>
                {
                    try
                    {
                        await taskRemovalSemaphore.WaitAsync();
                        tasks.Remove(completedTask);
                        if(completedTask.Exception != null)
                        {
                            OnException?.Invoke(this, completedTask.Exception);
                        }
                    }
                    finally
                    {
                        taskRemovalSemaphore.Release();
                    }
                });
            }
            else
            {
                throw new InvalidOperationException($"Cannot register work unless the { nameof(BackgroundWorkerService) } is started");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            cancellationToken = stoppingToken;
            cancellationToken.Register(() => IsStarted = false);

            // This loop will run for the entire lifetime of the owning application
            // until either a cancellation is requested or the StopAsync method is called.
            while (IsStarted)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            // To finish up gracefully we will first check if we have any remaining work.
            if(IsProcessingTasks)
            {
                // If we have any remaining work we will return a task that represents it.
                // Cancellation is managed in the inherited class.
                await Task.WhenAll(tasks);
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!IsStarted)
            {
                IsStarted = true;
                await base.StartAsync(cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (IsStarted)
            {
                IsStarted = false;
                await base.StopAsync(cancellationToken);
            }
        }

        public override void Dispose()
        {
            if(IsStarted)
            {
                IsStarted = false;
            }

            base.Dispose(); // Cancellationtoken will be called in inherited class.
        }
    }
}
