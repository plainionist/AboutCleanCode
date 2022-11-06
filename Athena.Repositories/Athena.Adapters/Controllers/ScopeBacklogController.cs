using Athena.Core.UseCases;

namespace Athena.Adapters.Controllers;

internal class ScopeBacklogController
{
    private ScopeBacklogUseCase myUseCase;

    public ScopeBacklogController(ScopeBacklogUseCase useCase)
    {
        myUseCase = useCase;
    }

    public ScopedBacklogVM ScopeBacklog()
    {
        // TODO: implement
        return null;
    }
}
