using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Depends.CLI
{
    internal class Workspace : IWorkspace
    {
        public Workspace(string root)
        {
            Root = root;

            if (!Directory.Exists(Root))
            {
                throw new Exception($"{Root} directory does not exist.");
            }
        }

        public string Root { get; }

        public IReadOnlyCollection<string> GetAllProjects() =>
            Directory.GetDirectories(Root)
                .SelectMany(x => Directory.GetFiles(x, "*proj", SearchOption.AllDirectories))
                .ToList();
    }
}