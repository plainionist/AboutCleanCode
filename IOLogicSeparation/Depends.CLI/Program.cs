
using System;
using System.IO;

namespace Depends.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0 || args.Length <= 2)
            {
                Console.WriteLine("Workspace root missing");
                return -1;
            }

            try
            {
                var workspace = new Workspace(args[0]);
                var projectLoader = new ProjectLoader();
                var reportWriter = new JsonReportWriter(
                    workspace,
                    Path.Combine(workspace.Root, "report.json"));

                var analyzer = new Analyzer(workspace, projectLoader, reportWriter);
                analyzer.Run();

                return 0;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}