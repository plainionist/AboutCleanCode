using System.Collections.Generic;
using Athena.Backlog.UseCases;

namespace Athena.Backlog.Tests
{
    internal class FakeWorkItemRepository : IWorkItemRepository
    {
        public IReadOnlyCollection<WorkItem> GetWorkItems() =>
            new[] {
                new WorkItem {
                    Id = 1,
                    Title = "User Story 1",
                    AssignedTo = "Bob",
                    State = WorkItemState.Ready
                },
                new WorkItem {
                    Id = 1,
                    Title = "User Story 2",
                    AssignedTo = "Olivia",
                    State = WorkItemState.Committed
                }
            };
    }
}