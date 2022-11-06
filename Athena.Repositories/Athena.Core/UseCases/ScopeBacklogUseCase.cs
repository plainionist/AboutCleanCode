namespace Athena.Core.UseCases;

public class ScopeBacklogUseCase
{
    private IBacklogRepository myRepository;

    public ScopeBacklogUseCase(IBacklogRepository repository)
    {
        myRepository = repository;
    }

    public ScopedBacklogResponse ScopeBacklog()
    {
        // TODO: implement
        return null;
    }
}
