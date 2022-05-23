using System.Collections.Generic;

namespace Naming
{
    public class Program2
    {
        private IEnumerable<string> ParseProcessIds(string processIds) =>
            processIds != null
                ? processIds.Split(',')
                : new List<string>();
    }
}