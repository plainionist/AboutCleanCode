using System.Threading;

namespace AboutCleanCode.Orchestrator;

class DataCollectorWorkerAgent : AbstractAgent
{
    public DataCollectorWorkerAgent(ILogger logger)
        : base(logger)
    {
        Receive<CollectDataCommand>(OnCollectDataCommand);
    }

    private void OnCollectDataCommand(IAgent sender, CollectDataCommand command)
    {
        var payload = CollectData();

        sender.Post(this, new DataCollectedEvent(command.JobId, payload));
    }

    // this takes a long time
    private object CollectData()
    {
        // TODO: implement

        for (int i = 0; i < 10; ++i)
        {
            Logger.Debug(this, $"Chunk {i}");
            Thread.Sleep(100);
        }

        return null;
    }
}