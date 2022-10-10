using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using NUnit.Framework;

namespace AboutCleanCode.Orchestrator.Tests;

public class OrchestratorTests
{
    [Test]
    public void DataCollectionCompletesSuccessfully()
    {
        var logger = new FakeLogger();

        var observer = new ObserverAgent(logger);
        observer.Start();

        var dataCollectorTask = new DataCollectorTask(logger);
        dataCollectorTask.Start();

        var orchestrator = new Orchestrator(logger, dataCollectorTask);
        orchestrator.StateObserver = observer;
        orchestrator.Start();

        var jobId = Guid.NewGuid();

        orchestrator.Post(orchestrator, new JobRequestReceivedMessage
        {
            Content = new XElement("JobRequest",
                new XElement("Id", jobId.ToString()),
                new XElement("CreatedAt", DateTime.Now))
                .ToString()
        });

        observer.JobStateChanged.WaitOne();

        dataCollectorTask.Stop();
        orchestrator.Stop();
        observer.Stop();

        Assert.True(logger.Messages.Any(x => x.Contains("DataCollectionStarted")), "DataCollectionStarted not found in logger");
        Assert.True(logger.Messages.Any(x => x.Contains("DataCollectionCompleted")), "DataCollectionCompleted not found in logger");
    }

    private class ObserverAgent : AbstractAgent
    {
        public ObserverAgent(ILogger logger) : base(logger) { }

        public ManualResetEvent JobStateChanged { get; } = new(false);

        protected override void OnReceive(IAgent sender, object message)
        {
            if (message is JobStateChanged _) {
                JobStateChanged.Set();
            }
        }
    }
}