using System.Collections.Generic;

namespace Depends.CLI
{
    internal interface IWorkspace
    {
        string Root { get; }

        IReadOnlyCollection<string> GetAllProjects();
    }
}