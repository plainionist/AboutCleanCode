using System.Collections.Generic;

namespace Athena.Core.Domain;

public class WorkPackage
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string IterationPath { get; init; }
    public string AreaPath { get; init; }
    public string AssignedTo { get; init; }
    public Improvement Improvement { get; init; }
    public IList<UserStory> WorkPackages { get; } = new List<UserStory>();
}
