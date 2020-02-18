using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CodeTech.Toolkit.Web.HostedServices
{
    public interface IBackgroundWorkerService
    {
        /// <summary>
        /// Runs the provided work on the threadpool
        /// </summary>
        /// <param name="work">Work to perform</param>
        void Run(Action<CancellationToken> work);
    }
}
