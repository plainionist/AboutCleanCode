using System;
using System.Threading;

namespace AboutCleanCode.Orchestrator;

class DataCollectorAgent : AbstractAgent
{
    public DataCollectorAgent(ILogger logger)
        : base(logger)
    {
        Receive<CollectDataCommand>(OnCollectDataCommand);
    }

    private void OnCollectDataCommand(IAgent sender, CollectDataCommand command)
    {
        try
        {
            sender.Post(this, new TaskStartedEvent(command.JobId));

            var payload = CollectData();

            sender.Post(this, new TaskCompletedEvent(command.JobId, payload));
        }
        catch (Exception exception)
        {
            sender.Post(this, new TaskFailedEvent(command.JobId, exception));
        }
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