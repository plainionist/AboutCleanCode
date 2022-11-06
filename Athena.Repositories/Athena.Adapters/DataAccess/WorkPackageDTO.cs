using System.Collections.Generic;

namespace Athena.Adapters.DataAccess;

public class WorkPackageDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string IterationPath { get; set; }
    public string AreaPath { get; set; }
    public string AssignedTo { get; set; }
    public ImprovementDTO Improvement { get; set; }
    public IList<UserStoryDTO> WorkPackages { get; } = new List<UserStoryDTO>();
}
