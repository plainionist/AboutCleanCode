using System.Collections.Generic;
using Athena.Backlog.UseCases;

namespace Athena.Backlog.Adapters.TestApi;

public class FakeWorkItemRepository : IWorkItemRepository
{
    public IReadOnlyCollection<WorkItem> GetWorkItems() =>
        new[] {
            new WorkItem {
                Id = 1,
                Title = "User Story 1",
                AssignedTo = new Developer("Bob", "bob@company.com"),
                State = WorkItemState.Ready
            },
            new WorkItem {
                Id = 1,
                Title = "User Story 2",
                AssignedTo = new Developer("Olivia", "oliver@company.com"),
                State = WorkItemState.Committed
            }
        };
}
