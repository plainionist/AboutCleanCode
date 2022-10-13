using System.Collections.Generic;
using System.Linq;

namespace AboutCleanCode.Orchestrator;

class DataCollectorAgent : AbstractAgent
{
    private record Request(IAgent Requester, IAgent Worker);

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
        sender.Post(this, new TaskStartedEvent(command.JobId));

        var worker = new DataCollectorWorkerAgent(Logger);
        worker.Start();

        myWorkers.Add(new Request(sender, worker));

        worker.Post(this, command);
    }

    private void OnDataCollectedEvent(IAgent worker, DataCollectedEvent evt)
    {
        var request = myWorkers.Single(x => x.Worker == worker);
        myWorkers.Remove(request);

        request.Requester.Post(this, new TaskCompletedEvent(evt.JobId, evt.Payload));
        worker.Post(this, new PoisonPill());
    }
}