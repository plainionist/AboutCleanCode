using System.Collections.Generic;

namespace Athena.Core.Domain;

public class Improvement
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string IterationPath { get; init; }
    public string AssignedTo { get; init; }
    public IList<WorkPackage> WorkPackages { get; } = new List<WorkPackage>();
}
