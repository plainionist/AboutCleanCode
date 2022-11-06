using System.Collections.Generic;
using Athena.Adapters.Controllers;
using Athena.Adapters.DataAccess;
using Athena.Core.UseCases;

namespace Athena.Adapters.TestApi;

public class ScopeBacklogApi
{
    public ScopedBacklogVM ScopeBacklog(IReadOnlyCollection<ImprovementDTO> workItems)
    {
        var repository = new BacklogRepository(new FakeDatabase(workItems));
        var controller = new ScopeBacklogController(new ScopeBacklogUseCase(repository));
        return controller.ScopeBacklog();
    }
}
