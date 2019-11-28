using System;

namespace CodeTech.Toolkit.Limits
{
    /// <summary>
    /// A synchronous limit used to limit the execution of specific lines of code.
    /// </summary>
    public interface ILimit
    {
        /// <summary>
        /// Blocks the current thread until the Limit deems it appropriate to continue.
        /// </summary>
        /// <returns>True if we entered the internal Semaphore, otherwise false.</returns>
        bool Wait();

        /// <summary>
        /// Blocks the current thread until the Limit deems it appropriate to continue or until the timeout is reached
        /// </summary>
        /// <param name="timeout">A timeout that indicates the maximum time you're willing to wait.</param>
        /// <returns>True if we entered the internal Semaphore, otherwise false.</returns>
        bool Wait(TimeSpan timeout);        
    }
}
