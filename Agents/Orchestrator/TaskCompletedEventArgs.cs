using System;

namespace AboutCleanCode.Orchestrator;

class TaskCompletedEventArgs : EventArgs
{
    public TaskCompletedEventArgs(Guid jobId, object payload)
    {
        JobId = jobId;
        Payload = payload;
    }

    public Guid JobId { get; }
    public object Payload { get; }
}