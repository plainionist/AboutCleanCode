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
        var orchestrator = new Orchestrator(logger, new DataCollectorTask());
        orchestrator.Start();

        var jobId = Guid.NewGuid();

        orchestrator.ProcessJobRequest(new XElement("JobRequest",
            new XElement("Id", jobId.ToString()),
            new XElement("CreatedAt", DateTime.Now))
            .ToString());

        WaitForJobCompleted(orchestrator, jobId);

        orchestrator.Stop();

        Assert.True(logger.Messages.Any(x => x.Contains("DataCollectionStarted")), "DataCollectionStarted not found in logger");
        Assert.True(logger.Messages.Any(x => x.Contains("DataCollectionCompleted")), "DataCollectionCompleted not found in logger");
    }

    private void WaitForJobCompleted(Orchestrator orchestrator, Guid jobId)
    {
        while (true)
        {
            Thread.Sleep(100);

            if (orchestrator.GetJobStatus(jobId).EndsWith("completed", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

        }
    }
}