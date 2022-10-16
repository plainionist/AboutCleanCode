using System.Collections.Generic;
using System.Linq;

namespace AboutCleanCode.Orchestrator;

class DataCollectorAgent : AbstractAgent
{
    private record Request(IAgent Requester, CollectDataCommand Command);
    private record Worker(IAgent Agent)
    {
        public Request ActiveRequest { get; set; }
    }

    private readonly Queue<Request> myRequests;
    private readonly List<Worker> myWorkers;
    private const int MaxWorker = 2;

    public DataCollectorAgent(ILogger logger)
        : base(logger)
    {
        myRequests = new();
        myWorkers = new();

        Receive<CollectDataCommand>(OnCollectDataCommand);
        Receive<DataCollectedEvent>(OnDataCollectedEvent);
        Receive<MessageProcessingFailedEvent>(OnMessageProcessingFailed);
    }

    private void OnMessageProcessingFailed(IAgent sender, MessageProcessingFailedEvent evt)
    {
        var worker = myWorkers.Single(x => x.Agent == sender);

        worker.ActiveRequest.Requester.Post(this, new TaskFailedEvent(worker.ActiveRequest.Command.JobId, evt.Exception));

        worker.ActiveRequest = null;

        DispatchNextRequest();
    }

    protected override void PostStart()
    {
        for (int i = 0; i < MaxWorker; ++i)
        {
            var worker = new DataCollectorWorkerAgent(Logger,this, i.ToString());
            worker.Start();
            myWorkers.Add(new Worker(worker));
        }
    }

    protected override void PreStop()
    {
        foreach (var worker in myWorkers)
        {
            worker.Agent.Post(this, new PoisonPill());
        }
    }

    private void OnCollectDataCommand(IAgent sender, CollectDataCommand command)
    {
        myRequests.Enqueue(new Request(sender, command));

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

        worker.ActiveRequest.Requester.Post(this, new TaskStartedEvent(worker.ActiveRequest.Command.JobId));

        worker.Agent.Post(this, worker.ActiveRequest.Command);
    }

    private void OnDataCollectedEvent(IAgent sender, DataCollectedEvent evt)
    {
        var worker = myWorkers.Single(x => x.Agent == sender);

        worker.ActiveRequest.Requester.Post(this, new TaskCompletedEvent(evt.JobId, evt.Payload));

        worker.ActiveRequest = null;

        DispatchNextRequest();
    }
}