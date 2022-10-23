using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator;

internal class OrchestratorAgent : AbstractAgent
{
    private readonly IAgentSystem myAgentSystem;
    private IAgent myDataCollectorTask;
    private IAgent myJobObserver;
    private readonly IDictionary<Guid, Job> myActiveJobs;

    internal OrchestratorAgent(ILogger logger, IAgentSystem agentSystem)
        : base(logger, "/user/orchestrator")
    {
        myAgentSystem = agentSystem;
        myActiveJobs = new Dictionary<Guid, Job>();

        Receive<JobRequestReceivedMessage>(OnJobRequestReceived);
        Receive<TaskStartedEvent>(OnDataCollectionStarted);
        Receive<TaskCompletedEvent>(OnDataCollectionCompleted);
        Receive<TaskFailedEvent>(OnDataCollectionFailed);
    }

    protected override void PostStart()
    {
        myDataCollectorTask = myAgentSystem.Select("/user/dataCollector");
        myJobObserver = myAgentSystem.TrySelect("/user/tests/jobObserver");
    }

    private void OnJobRequestReceived(IAgent _, JobRequestReceivedMessage command)
    {
        var job = ParseRequest(command.Content);
        myActiveJobs[job.Id] = job;

        // trigger collection of all relevant data before the data
        // of the job can be processed
        myDataCollectorTask.Post(this, new CollectDataCommand(job.Id));
    }

    private Job ParseRequest(string request)
    {
        var requestXml = XElement.Parse(request);
        return new Job(new Guid(requestXml.Element("Id").Value));
    }

    private void OnDataCollectionStarted(IAgent _, TaskStartedEvent e)
    {
        Logger.Info(this, $"OnDataCollectionStarted({e.JobId})");

        myActiveJobs[e.JobId].Status = "DataCollectionStarted";
    }

    private void OnDataCollectionCompleted(IAgent _, TaskCompletedEvent e)
    {
        Logger.Info(this, $"OnDataCollectionCompleted({e.JobId})");

        myActiveJobs[e.JobId].Status = "DataCollectionCompleted";

        // TODO: decide about next step and trigger its execution

        myJobObserver?.Post(this, new JobStateChanged(e.JobId));
    }

    private void OnDataCollectionFailed(IAgent _, TaskFailedEvent e)
    {
        Logger.Info(this, $"OnDataCollectionFailed({e.JobId})");

        myActiveJobs[e.JobId].Status = "DataCollectionFailed";

        // TODO: error handling - inform operators

        myJobObserver?.Post(this, new JobStateChanged(e.JobId));
    }
}