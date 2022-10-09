using System;

namespace AboutCleanCode.Orchestrator;

class TaskCompletedEvent 
{
    public TaskCompletedEvent(Guid jobId, object payload)
    {
        JobId = jobId;
        Payload = payload;
    }

    public Guid JobId { get; }
    public object Payload { get; }
}