using System;

namespace AboutCleanCode.Orchestrator;

internal class JobStateChanged
{
    public JobStateChanged(Guid jobId)
    {
        JobId = jobId;
    }

    public Guid JobId { get; }
}