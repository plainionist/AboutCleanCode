using System.Collections.Generic;

namespace Athena.Adapters.DataAccess;

public interface IDatabase
{
    IEnumerable<ImprovementDTO> GetBacklog();
}
