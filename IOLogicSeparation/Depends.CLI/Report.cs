using System.Collections.Generic;

namespace Depends.CLI
{
    class Report
    {
        public IReadOnlyCollection<VsProject> AllProjects { get; set; }
        public IReadOnlyCollection<VsProject> ProjectsWithDirectInvalidDeps { get; set; }
        public IReadOnlyCollection<VsProject> ProjectsWithIndirectInvalidDeps { get; set; }
    }
}
