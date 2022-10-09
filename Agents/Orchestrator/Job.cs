using System;

namespace AboutCleanCode.Orchestrator;

class Job
{
    public Job(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public string Status { get; set; }
}
