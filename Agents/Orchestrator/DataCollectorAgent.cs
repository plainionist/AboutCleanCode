using System;
using System.Collections.Generic;
using System.Linq;

namespace AboutCleanCode.Orchestrator;

class DataCollectorAgent : AbstractAgent
{
    private record Request(IAgent Requester, Guid JobId);
    private record Worker(AbstractAgent Agent)
    {
        public Request ActiveRequest { get; set; }
    }

    private const int MaxChildren = 2;

    private readonly Queue<Request> myRequests;
    private readonly List<Worker> myWorkers;

    public DataCollectorAgent(ILogger logger)
        : base(logger)
    {
        myRequests = new();
        myWorkers = new();

        Receive<CollectDataCommand>(OnCollectDataCommand);
        Receive<DataCollectedEvent>(OnDataCollectedEvent);
        Receive<MessageProcessingFailedEvent>(OnMessageProcessingFailed);
    }

    protected override void PostStart()
    {
        for (int i = 0; i < MaxChildren; ++i)
        {
            var worker = new DataCollectorWorkerAgent(this, Logger, i.ToString());
            worker.Start();
            myWorkers.Add(new Worker(worker));
        }
    }

    protected override void PreStop()
    {
        foreach (var worker in myWorkers)
        {
            worker.Agent.Stop();
        }
    }

    private void OnCollectDataCommand(IAgent sender, CollectDataCommand command)
    {
        myRequests.Enqueue(new Request(sender, command.JobId));

        DispatchNextRequest();
    }

    private void DispatchNextRequest()
    {
        if (!myRequests.Any())
        {
            return;
        }

        var worker = myWorkers.FirstOrDefault(x => x.ActiveRequest == null);
        if (worker == null)
        {
            return;
        }

        worker.ActiveRequest = myRequests.Dequeue();

        worker.ActiveRequest.Requester.Post(this, new TaskStartedEvent(worker.ActiveRequest.JobId));

        worker.Agent.Post(this, new CollectDataCommand(worker.ActiveRequest.JobId));
    }

    private void OnDataCollectedEvent(IAgent sender, DataCollectedEvent evt)
    {
        var worker = myWorkers.Single(x => x.Agent == sender);

        worker.ActiveRequest.Requester.Post(this, new TaskCompletedEvent(evt.JobId, evt.Payload));

        worker.ActiveRequest = null;

        DispatchNextRequest();
    }

    private void OnMessageProcessingFailed(IAgent sender, MessageProcessingFailedEvent evt)
    {
        var worker = myWorkers.Single(x => x.Agent == sender);

        worker.ActiveRequest.Requester.Post(this, new TaskFailedEvent(worker.ActiveRequest.JobId, evt.Exception));

        worker.ActiveRequest = null;

        DispatchNextRequest();
    }
}