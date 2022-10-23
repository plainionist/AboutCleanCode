using System;

namespace AboutCleanCode.Orchestrator;

internal class TaskStartedEvent 
{
    public TaskStartedEvent(Guid jobId)
    {
        JobId = jobId;
    }

    public Guid JobId { get; }
}