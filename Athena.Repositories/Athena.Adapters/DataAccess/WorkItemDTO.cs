using System.Collections.Generic;

namespace Athena.Adapters.DataAccess
{
    public class WorkItemDTO
    {
        public int Id { get; init; }
        
        public IReadOnlyDictionary<string, object> Fields { get; init; }
    }
}