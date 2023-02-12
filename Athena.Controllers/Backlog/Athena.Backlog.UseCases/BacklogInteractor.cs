namespace Athena.Backlog.UseCases;

public class BacklogInteractor
{
    private IWorkItemRepository myRepository;

    public BacklogInteractor(IWorkItemRepository repository)
    {
        myRepository = repository;
    }
    
    public BacklogResponseModel GetBacklog(BacklogRequestModel request)
    {
        var workItems = myRepository.GetWorkItems();

        return new BacklogResponseModel
        {
            WorkItems = workItems,
            TotalCapacity = 0.0, // TODO: to be calculated
            TotalEffort = 0.0 // TODO: to be calculated
        };
    }
}
