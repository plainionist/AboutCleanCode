using System.Linq;

namespace Athena.Adapters.DataAccess;

public interface IDatabase
{
    IQueryable<ImprovementDTO> GetBacklog();
}
