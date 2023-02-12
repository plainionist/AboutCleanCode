using System.Collections.Generic;

namespace Athena.Backlog.UseCases
{
    public interface IWorkItemRepository
    {
        IReadOnlyCollection<WorkItem> GetWorkItems();
    }
}