using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Web.HostedServices.Tests
{
    [TestClass]
    public class RunAsyncTests
    {
        /// <summary>
        /// Makes sure the service cannot register work until started
        /// </summary>
        [TestMethod]
        public void RejectWorkOnUnstartedService()
        {
            try
            {
                var service = new BackgroundWorkerService();
                Assert.ThrowsException<InvalidOperationException>(() =>
                {
                    service.RunAsync((cancellationToken) => Task.CompletedTask);
                }, "No exception was thrown");
            }
            catch (Exception ex) when (!(ex is InvalidOperationException) && !(ex is AssertFailedException))
            {
                Assert.Fail($"Wrong exception was thrown: '{ ex.GetType().Name }'");
            }
        }

        /// <summary>
        /// Makes sure the service starts correctly
        /// </summary>
        [TestMethod]
        public async Task StartService()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);
            Assert.IsTrue(service.IsStarted, "Service was not started");
        }

        /// <summary>
        /// Makes sure the service registrers work correctly
        /// </summary>
        [TestMethod]
        public async Task CheckStateOnRegistredWork()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);
            bool continueExecution = true;
            service.RunAsync(CreateTestTaskAsync(1000, token =>
            {
                return continueExecution;
            }));
            Assert.IsTrue(service.IsProcessingTasks, "Service did not start processing work");
            continueExecution = false;
        }

        /// <summary>
        /// Makes sure the service processes work correctly and updates its own state when finished
        /// </summary>
        [TestMethod]
        public async Task CheckStateOnFinishedWork()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);
            bool finishedExecuting = false;
            bool continueExecution = true;
            service.RunAsync(CreateTestTaskAsync(300, token =>
            {
                if (!continueExecution)
                {
                    finishedExecuting = true;
                }
                return continueExecution;
            }));

            // Our service should now be processing the task until we set the tas as finished
            Assert.IsTrue(service.IsProcessingTasks, "Service did not process the work");

            continueExecution = false; // We set the task as finished
            while (!finishedExecuting) // We wait until our work has noticed our flag
            {
                await Task.Delay(100);
            }
            
            // At this point our artificial work should have finished.
            // The service should no longer be processing work.
            Assert.IsFalse(service.IsProcessingTasks, "Service did not finish processing work");
        }

        // The test below needs to be reconsidered or redesigned.
        // We can only guarantee that the work is going to be scheduled on the thread pool.
        // We can not guarantee that it will be a separate thread (nor should we).

        ///// <summary>
        ///// Makes sure work assigned to be run in paralell actually runs in paralell
        ///// </summary>
        //[TestMethod]
        //public async Task CheckParalellWork()
        //{
        //    var cancellationTokenSource = new CancellationTokenSource();
        //    var service = new BackgroundWorkerService();
        //    await service.StartAsync(cancellationTokenSource.Token);
        //    int? workThreadId = null;
        //    service.Run(token =>
        //    {
        //        workThreadId = Thread.CurrentThread.ManagedThreadId;
        //    });

        //    while (!workThreadId.HasValue)
        //    {
        //        await Task.Delay(100);
        //    }

        //    Assert.AreNotEqual(Thread.CurrentThread.ManagedThreadId, workThreadId.Value, "Work was performed on the same thread as the service");
        //}

        /// <summary>
        /// Makes sure the service registrers stops correctly
        /// </summary>
        [TestMethod]
        public async Task StopService()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);
            await service.StopAsync(cancellationTokenSource.Token);
            Assert.IsFalse(service.IsStarted, "Service did not stop");
        }

        /// <summary>
        /// Makes sure the service allows work to finish gracefully if given the chance when stopped (StopAsync)
        /// </summary>
        [TestMethod]
        public async Task StopServiceWithWorkGracefulShutdown()
        {
            int stopCheckDelay = 300;
            var cancellationTokenSource = new CancellationTokenSource();
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);

            bool allowStop = false;
            bool cancellationWasCalled = false;
            service.RunAsync(CreateTestTaskAsync(stopCheckDelay, (token) =>
            {
                // If we want to stop the work
                if (allowStop)
                {
                    cancellationWasCalled = token.IsCancellationRequested;
                    return false;
                }

                return true;
            }));

            // We will stop the service, this should not stop the work from finishing
            // We cannot await this task at this point in time as it will not be able to finish until stopToken.IsStopped is set further down (deadlock).
            // We can call await after we have stopped the work to view the status
            var stoppingTask = service.StopAsync(cancellationTokenSource.Token);

            Assert.IsFalse(service.IsStarted, "Service was still runing despite being told to stop");
            Assert.IsTrue(service.IsProcessingTasks, "Service stopped processing unfinished work when stopped");

            // Now we will simulate our work finishing.
            allowStop = true;
            var waitDelay = (stopCheckDelay * 2) + 1;
            await Task.Delay(waitDelay); // We will wait until we are 100% sure our testwork has registered the stop.
            Assert.IsFalse(service.IsProcessingTasks, $"Service was still processing work { waitDelay } milliseconds after stopping");
            await stoppingTask;
            Assert.IsTrue(stoppingTask.IsCompletedSuccessfully, "Stopping task did not complete successfully");
            Assert.IsTrue(cancellationWasCalled, "Cancellation was not called");
        }

        /// <summary>
        /// Makes sure the service cancels work when an ungraceful shutdown happens (Dispose)
        /// </summary>
        [TestMethod]
        public async Task StopServiceWithWorkUngracefulShutdown()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            int stopCheckDelay = 300;
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);
            bool allowStop = false;
            bool cancellationWasCalled = false;
            var workTask = CreateTestTaskAsync(stopCheckDelay, cancellationToken =>
            {
                if (allowStop)
                {
                    cancellationWasCalled = cancellationToken.IsCancellationRequested;
                    return false;
                }

                return true;
            });
            service.RunAsync(workTask);
            service.Dispose();
            Assert.IsFalse(service.IsStarted, "Service was still runing despite being disposed");
            Assert.IsTrue(service.IsProcessingTasks, "Service was not processing unfinished work when disposed");
            allowStop = true;
            var waitDelay = stopCheckDelay * 2;
            await Task.Delay(waitDelay); // Make sure our work has a chance to react
            Assert.IsFalse(service.IsProcessingTasks, $"Service was still processing work { waitDelay } milliseconds after stopping");
            Assert.IsTrue(cancellationWasCalled, "Cancellation was not called");
        }

        /// <summary>
        /// Makes sure the service forwards exceptions thrown in provided work to the proper eventhandler.
        /// </summary>
        [TestMethod]
        public async Task ExceptionEventWithSubscriber()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);
            bool exceptionCallbackWasCalled = false;
            service.OnException += (sender, e) =>
            {
                exceptionCallbackWasCalled = true;
            };
            service.RunAsync(async token =>
            {
                throw new Exception("Win!");
            });

            await Task.Delay(100); // Since our test task does not contain anything asynchronous we will have to wait for it to execute

            Assert.IsTrue(exceptionCallbackWasCalled, "Exception event was not raised");
        }

        /// <summary>
        /// Makes sure the service does not explode (violently) when provided work throws an exception and no eventhandlers are present
        /// </summary>
        [TestMethod]
        public async Task ExceptionEventWithoutSubscriber()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var service = new BackgroundWorkerService();
            await service.StartAsync(cancellationTokenSource.Token);
            service.RunAsync(async token =>
            {
                throw new Exception("Win!");
            });

            await Task.Delay(100); // Since our test task does not contain anything asynchronous we will have to wait for it to execute
            // We only need to determine that the exception does not disrupt the application outside of the task in progress
        }

        private Func<CancellationToken, Task> CreateTestTaskAsync(int millisecondDelayBetweenChecks, Func<CancellationToken, bool> actionBetweenChecks)
        {
            return async (cancellationToken) =>
            {
                bool continueWorking = true;
                while (continueWorking)
                {
                    await Task.Delay(millisecondDelayBetweenChecks);
                    continueWorking = actionBetweenChecks(cancellationToken);
                }
            };
        }
    }
}
