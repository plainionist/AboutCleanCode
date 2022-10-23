using System;
using System.Threading;

namespace AboutCleanCode.Orchestrator;

class DataCollectorWorkerAgent : AbstractAgent
{
    private readonly string myName;

    public DataCollectorWorkerAgent(ILogger logger, IAgent supervisor, string name)
        : base(logger, $"/user/dataCollector/worker/{name}", supervisor)
    {
        myName = name;

        Receive<CollectDataCommand>(OnCollectDataCommand);
    }

    private void OnCollectDataCommand(IAgent sender, CollectDataCommand command)
    {
        var payload = CollectData(command.JobId);

        sender.Post(this, new DataCollectedEvent(command.JobId, payload));
    }

    // this takes a long time
    private object CollectData(Guid jobId)
    {
        if (jobId.ToString() == "991bb0c7-b0d7-4fcb-8c79-cbb55613e772")
        {
            throw new Exception("Ups!");
        }

        // TODO: implement

        for (int i = 0; i < 10; ++i)
        {
            Logger.Debug(this, $"{myName}|Chunk {i}");
            Thread.Sleep(100);
        }

        return null;
    }
}