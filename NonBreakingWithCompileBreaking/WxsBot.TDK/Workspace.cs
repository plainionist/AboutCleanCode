using System;
using System.Collections.Generic;
using System.IO;
using WxsBot.Entities;

namespace WxsBot.TDK
{
    public class Workspace : IDisposable
    {
        private Workspace(string root)
        {
            Root = root;
        }

        public string Root { get; }

        public Version WxsVersion { get; }

        public Bundle Bundle { get; private set; }

        public Dictionary<string, ICSharpProject> Projects { get; private set; }

        public ContentFile ContentFile { get; private set; }

        public static Workspace CreateFromTemplate()
        {
            var workspace = new Workspace(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            Directory.CreateDirectory(workspace.Root);

            return workspace;
        }

        public void Dispose()
        {
            if (Directory.Exists(Root))
            {
                Directory.Delete(Root, true);
            }
        }

        public void AddContentFile(string projectName, FilePath targetPath, string fileName)
        {
            ContentFile = Projects[projectName].AddContentFile(targetPath.ToString(), fileName);
        }

        public void AddSourceLibraryProjects(params string[] projectNames)
        {
            Projects = Bundle.AddSourceProjects(true, projectNames);
        }

        public void AddSourceExeProjects(params string[] projectNames)
        {
            Projects = Bundle.AddSourceProjects(true, projectNames);
        }

        public void CreateBundle(string name, bool useSdkStyleProjects)
        {
            Bundle = Bundle.Create(Root, name, useSdkStyleProjects);
        }
    }
}
