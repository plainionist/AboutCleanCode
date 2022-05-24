using System.Collections.Generic;

namespace Depends.CLI
{
    class VsProject
    {
        public string AssemblyName { get; set; }
        public string Location { get; set; }
        public IReadOnlyCollection<string> References { get; set; } = new List<string>();
    }
}
