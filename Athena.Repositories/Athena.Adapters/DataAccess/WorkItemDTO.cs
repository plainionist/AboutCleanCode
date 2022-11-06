using System.Collections.Generic;

namespace Athena.Adapters.DataAccess
{
    public class WorkItemDTO
    {
        public int? Id { get; init; }
        
        public IDictionary<string, object> Fields { get; init; }
    }
}