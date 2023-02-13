namespace Athena.Backlog.UseCases;

public class WorkItem
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string AssignedTo { get; init; }
    public WorkItemState State { get; init; }
}