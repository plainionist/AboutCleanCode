using System.Collections.Generic;

namespace Athena.Adapters.DataAccess;

public class ImprovementDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string IterationPath { get; set; }
    public string AssignedTo { get; set; }
    public IList<WorkPackageDTO> WorkPackages { get; } = new List<WorkPackageDTO>();
}
