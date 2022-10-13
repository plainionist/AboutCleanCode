using System;

namespace AboutCleanCode.Orchestrator;

internal class DataCollectedEvent
{
    public DataCollectedEvent(Guid jobId, object payload)
    {
        JobId = jobId;
        Payload = payload;
    }

    public Guid JobId { get; }
    public object Payload { get; }
}