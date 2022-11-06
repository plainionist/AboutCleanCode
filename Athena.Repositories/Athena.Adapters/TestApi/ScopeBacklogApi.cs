using System.Collections.Generic;
using Athena.Adapters.Controllers;
using Athena.Core.UseCases;

namespace Athena.Adapters.TestApi;

public class ScopeBacklogApi
{
    public ScopedBacklogVM ScopeBacklog(IReadOnlyCollection<WorkItem> workItems)
    {
        var repository = new FakeBacklogRepository(workItems);
        var controller = new ScopeBacklogController(new ScopeBacklogUseCase(repository));
        return controller.ScopeBacklog();
    }
}
