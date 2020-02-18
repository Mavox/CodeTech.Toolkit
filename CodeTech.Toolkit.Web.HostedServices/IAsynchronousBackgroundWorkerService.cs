using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Web.HostedServices
{
    public interface IAsynchronousBackgroundWorkerService
    {
        /// <summary>
        /// Runs the provided task asynchronously
        /// </summary>
        /// <param name="work">Work to perform</param>
        void RunAsync(Func<CancellationToken, Task> work);
    }
}
