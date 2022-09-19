using System.Collections.Generic;

namespace Athena.Web.Fakes;

public class FakeWorkItemRepository : Athena.Backlog.UseCases.IWorkItemRepository
{
    public IReadOnlyCollection<Athena.Backlog.UseCases.WorkItem> GetWorkItems()
    {
        return null;
    }
}
