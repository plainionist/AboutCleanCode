using System.Collections.Generic;

namespace Athena.Adapters.DataAccess;

public interface ISqlDatabase
{
    IEnumerable<WorkItemDTO> Query(string sql);
    void Execute(string sql);
}
