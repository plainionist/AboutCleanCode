using System;
using System.Collections.Generic;
using System.Linq;

namespace AboutCleanCode.Orchestrator;

class DataCollectorAgent : AbstractAgent
{
    private record Request(IAgent Requester, Guid JobId)
    {
        public bool IsRunning => Worker != null;
        public IAgent Worker { get; set; }
    }

    private int myWorkerIdSeq = 0;
    private const int MaxChildren = 2;

    private readonly List<Request> myWorkers;

    public DataCollectorAgent(ILogger logger)
        : base(logger)
    {
        myWorkers = new();

        Receive<CollectDataCommand>(OnCollectDataCommand);
        Receive<DataCollectedEvent>(OnDataCollectedEvent);
    }

    private void OnCollectDataCommand(IAgent sender, CollectDataCommand command)
    {
        var request = new Request(sender, command.JobId);
        myWorkers.Add(request);

        if (myWorkers.Count(x => x.IsRunning) < MaxChildren)
        {
            StartChild(request);
        }
    }

    private void StartChild(Request request)
    {
        request.Requester.Post(this, new TaskStartedEvent(request.JobId));

        var worker = new DataCollectorWorkerAgent(Logger, (myWorkerIdSeq++).ToString());
        worker.Start();

        worker.Post(this, new CollectDataCommand(request.JobId));

        request.Worker = worker;
    }

    private void OnDataCollectedEvent(IAgent worker, DataCollectedEvent evt)
    {
        var request = myWorkers.Single(x => x.Worker == worker);

        request.Requester.Post(this, new TaskCompletedEvent(evt.JobId, evt.Payload));
        worker.Post(this, new PoisonPill());

        myWorkers.Remove(request);

        var nextRequest = myWorkers.FirstOrDefault(x => !x.IsRunning);
        if (nextRequest != null)
        {
            StartChild(nextRequest);
        }
    }
}