using System.Collections.Generic;

namespace Athena.Backlog.IO.V2;

public class BacklogVM
{
    public IReadOnlyCollection<WorkItemVM> WorkItems { get; init; }
    public string TotalEffort { get; init; }
    public string TotalCapacity { get; init; }
}

public class WorkItemVM
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string AssignedTo { get; init; }
    public string State { get; init; }
}
