namespace Athena.Core.Domain;

public class UserStory
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string IterationPath { get; init; }
    public string AreaPath { get; init; }
    public string AssignedTo { get; init; }
    public double RemainingWork { get; init; }
    public double CompletedWork { get; init; }
    public WorkPackage WorkPackage { get; init; }
}
