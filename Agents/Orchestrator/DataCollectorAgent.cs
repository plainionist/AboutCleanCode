using System;

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

            // TODO: collect all necessary data which takes quite some time

            object payload = null; // TODO: carries the collected data

            sender.Post(this, new TaskCompletedEvent(jobId, payload));
        }
        catch (Exception exception)
        {
            sender.Post(this, new TaskFailedEvent(jobId, exception));
        }
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