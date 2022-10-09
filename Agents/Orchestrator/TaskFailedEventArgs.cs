using System;

namespace AboutCleanCode.Orchestrator;

class TaskFailedEvent 
{
    public TaskFailedEvent(Guid jobId, Exception exception)
    {
        JobId = jobId;
        Exception = exception;
    }

    public Guid JobId { get; }
    public Exception Exception { get; }
}
