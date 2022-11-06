
namespace Athena.Adapters.DataAccess;

public class UserStoryDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string IterationPath { get; set; }
    public string AreaPath { get; set; }
    public string AssignedTo { get; set; }
    public double RemainingWork { get; set; }
    public double CompletedWork { get; set; }
    public WorkPackageDTO WorkPackage { get; set; }
}
