using System.Collections.Generic;
using System.Linq;
using Athena.Backlog.UseCases;

namespace Athena.Web.Fakes;

public class FakeWorkItemRepository : IWorkItemRepository
{
    public IReadOnlyCollection<WorkItem> GetWorkItems()
    {
        return Enumerable.Range(1, 5).Select(index => new WorkItem
        {
            Id = index,
            Title = $"Title {index}",
            AssignedTo = $"Developer {index}"
        })
        .ToArray();
    }
}
