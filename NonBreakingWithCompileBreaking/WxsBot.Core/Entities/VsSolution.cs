using System.Collections.Generic;

namespace WxsBot.Entities
{
    public class VsSolution
    {
        public VsSolution(string path, IReadOnlyCollection<VsProject> projects)
        {
            Contract.RequiresNotNullNotEmpty(path, nameof(path));
            Contract.RequiresNotNull(projects, nameof(projects));

            FilePath = path;
            Projects = projects;
        }

        public string FilePath { get; }

        public IReadOnlyCollection<VsProject> Projects { get; }
    }
}
