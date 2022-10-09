using System;

namespace AboutCleanCode.Orchestrator;

class TaskFailedEventArgs : EventArgs
{
    public TaskFailedEventArgs(Guid jobId, Exception exception)
    {
        JobId = jobId;
        Exception = exception;
    }

    public Guid JobId { get; }
    public Exception Exception { get; }
}
