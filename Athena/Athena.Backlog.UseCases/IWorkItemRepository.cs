namespace Athena.Backlog.UseCases
{
    public interface IWorkItemRepository
    {
        IReadOnlyCollection<WorkItem> GetWorkItems();
    }
}