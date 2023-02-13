using System.Collections.Generic;

namespace Athena.Backlog.Adapters;

public class BacklogVM
{
    public IReadOnlyCollection<WorkItemVM> WorkItems { get; init; }
    public string TotalEffort { get; init; }
    public string TotalCapacity { get; init; }
}