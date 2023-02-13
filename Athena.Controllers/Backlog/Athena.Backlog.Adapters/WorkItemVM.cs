namespace Athena.Backlog.Adapters
{
    public class WorkItemVM
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string AssignedTo { get; init; }
        public string State { get; init; }
    }
}