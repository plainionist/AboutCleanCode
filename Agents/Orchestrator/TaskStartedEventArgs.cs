using System;

namespace AboutCleanCode.Orchestrator;

internal class TaskStartedEventArgs : EventArgs
{
    public TaskStartedEventArgs(Guid jobId)
    {
        JobId = jobId;
    }

    public Guid JobId { get; }
}