namespace Athena.Core.Domain;

public class UserStory
{
    public UserStory(int id, string title, string description, IterationPath iterationPath, 
        AreaPath areaPath, EMail assignedTo, double remainingWork, double completedWork, WorkPackage workPackage)
    {
        Id = id;
        Title = title;
        Description = description;
        IterationPath = iterationPath;
        AreaPath = areaPath;
        AssignedTo = assignedTo;
        RemainingWork = remainingWork;
        CompletedWork = completedWork;
        WorkPackage = workPackage;
    }

    public int Id { get; }
    public string Title { get; }
    public string Description { get; }
    public IterationPath IterationPath { get; }
    public AreaPath AreaPath { get; }
    public EMail AssignedTo { get; }
    public double RemainingWork { get; }
    public double CompletedWork { get; }
    public WorkPackage WorkPackage { get; }
}
