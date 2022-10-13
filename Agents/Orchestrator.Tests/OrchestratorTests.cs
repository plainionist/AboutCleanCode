using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using NUnit.Framework;

namespace AboutCleanCode.Orchestrator.Tests;

public class OrchestratorTests
{
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void DataCollectionCompletesSuccessfully(int numJobs)
    {
        var logger = new FakeLogger();

        var jobIds = Enumerable.Range(0, numJobs)
            .Select(x => Guid.NewGuid())
            .ToList();

        var observer = new JobObserverAgent(logger, jobIds);
        observer.Start();

        var dataCollectorTask = new DataCollectorAgent(logger);
        dataCollectorTask.Start();

        var orchestrator = new OrchestratorAgent(logger, dataCollectorTask);
        orchestrator.JobObserver = observer;
        orchestrator.Start();

        foreach (var jobId in jobIds)
        {
            orchestrator.Post(orchestrator, new JobRequestReceivedMessage
            {
                Content = new XElement("JobRequest",
                    new XElement("Id", jobId.ToString()),
                    new XElement("CreatedAt", DateTime.Now))
                    .ToString()
            });
        }

        observer.AllJobsProcessed.WaitOne();

        dataCollectorTask.Stop();
        orchestrator.Stop();
        observer.Stop();

        foreach (var jobId in jobIds)
        {
            Assert.True(logger.Messages.Any(x => x.Contains($"DataCollectionStarted({jobId})")), "DataCollectionStarted not found in logger");
            Assert.True(logger.Messages.Any(x => x.Contains($"DataCollectionCompleted({jobId})")), "DataCollectionCompleted not found in logger");
        }
    }

    private class JobObserverAgent : AbstractAgent
    {
        private IList<Guid> myRemainingJobs;

        public JobObserverAgent(ILogger logger, IEnumerable<Guid> jobsToObserve)
            : base(logger)
        {
            myRemainingJobs = jobsToObserve.ToList();

            Receive<JobStateChanged>(OnJobStateChanged);
        }

        private void OnJobStateChanged(IAgent _, JobStateChanged evt)
        {
            myRemainingJobs.Remove(evt.JobId);
            
            if (myRemainingJobs.Count == 0)
            {
                AllJobsProcessed.Set();
            }
        }

        public ManualResetEvent AllJobsProcessed { get; } = new(false);
    }
}