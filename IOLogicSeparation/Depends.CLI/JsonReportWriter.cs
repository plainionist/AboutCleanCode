using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Depends.CLI
{
    internal class JsonReportWriter : IReportWriter
    {
        private readonly IWorkspace myWorkspace;
        private readonly string myOutput;

        public JsonReportWriter(IWorkspace workspace, string output)
        {
            myWorkspace = workspace;
            myOutput = output;
        }

        public void Write(Report report)
        {
            var dto = new ReportDto
            {
                TotalProjects = report.AllProjects.Count,

                ProjectsWithDirectInvalidDeps = report.ProjectsWithDirectInvalidDeps
                    .Select(x => x.Location.Substring(myWorkspace.Root.Length + 1))
                    .ToList(),
                ProjectsWithIndirectInvalidDeps = report.ProjectsWithIndirectInvalidDeps
                    .Select(x => x.Location.Substring(myWorkspace.Root.Length + 1))
                    .ToList(),
            };

            WriteJsonFile(dto, myOutput);
        }

        class ReportDto
        {
            public int TotalProjects { get; set; }
            public IReadOnlyCollection<string> ProjectsWithDirectInvalidDeps { get; set; }
            public IReadOnlyCollection<string> ProjectsWithIndirectInvalidDeps { get; set; }
        }

        private void WriteJsonFile<T>(T data, string path)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(path, json);
        }
    }
}
