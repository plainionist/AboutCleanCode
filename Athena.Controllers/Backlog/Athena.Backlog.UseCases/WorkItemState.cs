namespace Athena.Backlog.UseCases;

public enum WorkItemState
{
    Created,
    Ready,
    Committed,
    InWork,
    Done,
    Terminated
}