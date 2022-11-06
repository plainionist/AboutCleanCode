using System.Collections.Generic;

namespace Athena.Core.Domain;

public class Improvement
{
    public Improvement(int id, string title, string description, IterationPath iterationPath,
        EMail assignedTo, IReadOnlyList<WorkPackage> workPackages)
    {
        Id = id;
        Title = title;
        Description = description;
        IterationPath = iterationPath;
        AssignedTo = assignedTo;
        WorkPackages = workPackages;
    }

    public int Id { get; }
    public string Title { get; }
    public string Description { get; }
    public IterationPath IterationPath { get; }
    public EMail AssignedTo { get; }
    public IReadOnlyList<WorkPackage> WorkPackages { get; }
}
