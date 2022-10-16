using System.Collections.Generic;
using System.Linq;

namespace AboutCleanCode.Orchestrator;

class DataCollectorAgent : AbstractAgent
{
    private record Request(IAgent Requester, IAgent Worker);
    private readonly List<Request> myRequests;
    private int myWorkerIdSeq = 0;

    public DataCollectorAgent(ILogger logger)
        : base(logger)
    {
        myRequests = new();

        Receive<CollectDataCommand>(OnCollectDataCommand);
        Receive<DataCollectedEvent>(OnDataCollectedEvent);
    }

    private void OnCollectDataCommand(IAgent sender, CollectDataCommand command)
    {
        sender.Post(this, new TaskStartedEvent(command.JobId));

        var worker = new DataCollectorWorkerAgent(Logger, (myWorkerIdSeq++).ToString());
        worker.Start();

        myRequests.Add(new Request(sender, worker));

        worker.Post(this, command);
    }

    private void OnDataCollectedEvent(IAgent sender, DataCollectedEvent evt)
    {
        var request = myRequests.Single(x => x.Worker == sender);
        myRequests.Remove(request);

        request.Requester.Post(this, new TaskCompletedEvent(evt.JobId,evt.Payload));

        sender.Post(this, new PoisonPill());
    }
}