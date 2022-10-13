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

        // setup all agents

        var observer = new JobObserverAgent(logger, jobIds);
        observer.Start();

        var dataCollectorTask = new DataCollectorAgent(logger);
        dataCollectorTask.Start();

        var orchestrator = new OrchestratorAgent(logger, dataCollectorTask);
        orchestrator.JobObserver = observer;
        orchestrator.Start();

        // queue all jobs

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

        // wait for all jobs to finish

        observer.AllJobsProcessed.WaitOne();

        // stop "agent system"

        dataCollectorTask.Stop();
        orchestrator.Stop();
        observer.Stop();

        // for diagnosis purpose
        foreach (var messages in logger.Messages)
        {
            Console.WriteLine(messages);
        }

        // verify proper processing of the jobs
        foreach (var jobId in jobIds)
        {
            Assert.True(logger.Messages.Any(x => x.Contains($"DataCollectionStarted({jobId})")), "DataCollectionStarted not found in logger");
            Assert.True(logger.Messages.Any(x => x.Contains($"DataCollectionCompleted({jobId})")), "DataCollectionCompleted not found in logger");
        }
    }

    /// <summary>
    /// Raises an event when all jobs to observe are processed.
    /// </summary>
    private class JobObserverAgent : AbstractAgent
    {
        private readonly IList<Guid> myRemainingJobs;

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