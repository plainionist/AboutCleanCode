using System.Collections.Generic;
using Athena.Core.Domain;
using Athena.Core.UseCases;

namespace Athena.Adapters.TestApi;

internal class FakeBacklogRepository : IBacklogRepository
{
    private IReadOnlyCollection<WorkItem> myWorkItems;

    public FakeBacklogRepository(IReadOnlyCollection<WorkItem> workItems)
    {
        myWorkItems = workItems;
    }

    public IReadOnlyCollection<Improvement> GetBacklog()
    {
        // TODO: implement
        return null;
    }
}