using System.Collections.Generic;
using System.IO;
using WxsBot.Gateways.MsBuild;

namespace WxsBot.TDK
{
    public class Bundle
    {
        private Bundle(string root, string name, bool useSdkStyleProjects)
        {
            Name = name;
            Location = Path.Combine(root, Name);
            UseSdkStyleProjects = useSdkStyleProjects;
        }

        public string Name { get; }

        public string Location { get; }

        public bool UseSdkStyleProjects { get; }

        public static Bundle Create(string root, string name, bool useSdkStyleProjects)
        {
            Directory.CreateDirectory(Path.Combine(root, name));

            var bundle = new Bundle(root, name, useSdkStyleProjects);

            bundle.GenerateBundleSolution();

            return bundle;
        }

        public Dictionary<string, ICSharpProject> AddSourceProjects(bool isLibrary, params string[] projectNames)
        {
            Dictionary<string, ICSharpProject> projects = new Dictionary<string, ICSharpProject>();

            var directoryName = "Source";
            foreach (var name in projectNames)
            {
                projects.Add(name, new CSharpProjectFactory(UseSdkStyleProjects).Create(Path.Combine(Location, directoryName), name, isLibrary));
            }

            GenerateBundleSolution();

            return projects;
        }

        private void GenerateBundleSolution()
        {
            VisualStudioSolutionFacade.Create(
                Directory.GetFiles(Location, "*.csproj", SearchOption.AllDirectories),
                Path.Combine(Location, $"{Name}.sln"));
        }
    }
}