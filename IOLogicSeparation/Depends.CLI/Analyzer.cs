using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Depends.CLI
{
    class Analyzer
    {
        private readonly IWorkspace myWorkspace;
        private readonly IProjectLoader myProjectLoader;
        private readonly IReportWriter myReportWriter;

        public Analyzer(IWorkspace workspace, IProjectLoader projectLoader, IReportWriter reportWriter)
        {
            myWorkspace = workspace;
            myProjectLoader = projectLoader;
            myReportWriter = reportWriter;
        }

        public void Run()
        {
            var projects = LoadProjects();

            var projectsWithInvalidDeps = FindProjectsWithInvalidDeps(projects);

            var projectsWithValidDeps = projects.Except(projectsWithInvalidDeps).ToList();

            var projectsWithIndirectInvalidDeps = FindProjectsDependingOnProjectsWithInvalidDeps(
                projectsWithValidDeps, projectsWithInvalidDeps);

            var report = new Report
            {
                AllProjects = projects,
                ProjectsWithDirectInvalidDeps = projectsWithInvalidDeps,
                ProjectsWithIndirectInvalidDeps = projectsWithIndirectInvalidDeps
            };

            myReportWriter.Write(report);
        }

        private static IReadOnlyCollection<VsProject> FindProjectsDependingOnProjectsWithInvalidDeps(
            IReadOnlyCollection<VsProject> projectsWithValidDeps,
            IReadOnlyCollection<VsProject> projectsWithIndirectInvalidDeps)
        {
            var projsWithIndirectInvalidDeps = new List<VsProject>();

            // TODO: recursive search logic to be implemented here

            return projsWithIndirectInvalidDeps
                .Distinct(new CsProjectComparer())
                .ToList();
        }

        private class CsProjectComparer : IEqualityComparer<VsProject>
        {
            public bool Equals(VsProject x, VsProject y) =>
                x != null && y != null && x.Location.Equals(y.Location, StringComparison.OrdinalIgnoreCase);

            public int GetHashCode(VsProject obj) =>
                obj.Location.ToLower().GetHashCode();
        }

        private static IReadOnlyCollection<VsProject> FindProjectsWithInvalidDeps(IEnumerable<VsProject> projects) =>
            projects.Where(x => HasInvalidDependencies(x.Location)).ToList();

        private IReadOnlyCollection<VsProject> LoadProjects() =>
             myWorkspace.GetAllProjects()
                .Where(x => ".csproj".Equals(Path.GetExtension(x), StringComparison.OrdinalIgnoreCase))
                .Where(x => !x.Contains("\\Test\\"))
                .Select(x => myProjectLoader.LoadProject(x))
                .ToList();

        public static bool HasInvalidDependencies(string csproj)
        {
            // TODO: to be implemented 
            return false;
        }
    }
}