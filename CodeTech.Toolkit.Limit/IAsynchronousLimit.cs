using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Limits
{
    /// <summary>
    /// An asynchronous limit used to limit the execution of specific lines of code.
    /// </summary>
    public interface IAsynchronousLimit
    {
        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue.
        /// </summary>
        /// <returns>A task that finishes when it is ok to continue.</returns>
        Task<bool> WaitAsync();

        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue or until the timeout is reached.
        /// </summary>
        /// <param name="timeout">A timeout that indicates the maximum time you're willing to wait.</param>
        /// <returns>A task that finishes when it is ok to continue or when the timeout is reached.</returns>
        Task<bool> WaitAsync(TimeSpan timeout);

        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue while observing a cancellationtoken.
        /// </summary>
        /// <param name="cancellationToken">Cancellationtoken to observe.</param>
        /// <returns>A task that finishes when it is ok to continue.</returns>
        Task<bool> WaitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Waits asynchronously until the Limit deems it appropriate to continue or until the timeout is reached while observing a cancellationtoken.
        /// </summary>
        /// <param name="timeout">A timeout that indicates the maximum time you're willing to wait.</param>
        /// <param name="cancellationToken">Cancellationtoken to observe.</param>
        /// <returns>A task that finishes when it is ok to continue or when the timeout is reached.</returns>
        Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken);
    }
}
