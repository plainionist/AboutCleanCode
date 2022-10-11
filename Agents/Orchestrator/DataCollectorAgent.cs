using System;
using System.Threading;

namespace AboutCleanCode.Orchestrator;

class DataCollectorAgent : AbstractAgent
{
    public DataCollectorAgent(ILogger logger)
        : base(logger)
    { }

    private void Process(IAgent sender, Guid jobId)
    {
        try
        {
            sender.Post(this, new TaskStartedEvent(jobId));

            var payload = CollectData();

            sender.Post(this, new TaskCompletedEvent(jobId, payload));
        }
        catch (Exception exception)
        {
            sender.Post(this, new TaskFailedEvent(jobId, exception));
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

    protected override void OnReceive(IAgent sender, object message)
    {
        if (message is CollectDataCommand cdc)
        {
            Process(sender, cdc.JobId);
        }
        else
        {
            Logger.Warning(this, $"Unknown message: '{message.GetType().FullName}'");
        }
    }
}