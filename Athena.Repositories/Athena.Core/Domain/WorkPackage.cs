using System.Collections.Generic;

namespace Athena.Core.Domain;

public class WorkPackage
{
    public WorkPackage(int id, string title, string description, IterationPath iterationPath,
        AreaPath areaPath, EMail assignedTo, Improvement improvement,
        IReadOnlyCollection<UserStory> workPackages)
    {
        Id = id;
        Title = title;
        Description = description;
        IterationPath = iterationPath;
        AreaPath = areaPath;
        AssignedTo = assignedTo;
        Improvement = improvement;
        WorkPackages = workPackages;
    }

    public int Id { get; }
    public string Title { get; }
    public string Description { get; }
    public IterationPath IterationPath { get; }
    public AreaPath AreaPath { get; }
    public EMail AssignedTo { get; }
    public Improvement Improvement { get; }
    public IReadOnlyCollection<UserStory> WorkPackages { get; }
}
