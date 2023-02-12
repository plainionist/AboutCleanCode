using System.Collections.Generic;

namespace Athena.Backlog.UseCases
{
    public class BacklogResponseModel
    {
        public IReadOnlyCollection<WorkItem> WorkItems { get; init; }
        public double TotalEffort { get; init; }
        public double TotalCapacity { get; init; }
    }
}