using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestFailureAnalyzer.CLI
{
    public class CommandLineOptions 
    {
        public CommandLineOptions()
        {
            ShowHelp = false;
            IsDryRun = false;
        }

        public string BuildNumber { get; set; }
        public bool IsDryRun { get; set; }
        public bool ShowHelp { get; set; }
    }

    public class CommandLineParser
    {
        public CommandLineOptions Parse(IReadOnlyList<string> args)
        {
            var options = new CommandLineOptions();

            if (args.Count == 0)
            {
                options.ShowHelp = true;
                return options;
            }

            foreach (var arg in args)
            {
                bool IsOption(string option) =>
                        arg.Equals($"-{option}", StringComparison.OrdinalIgnoreCase);

                if (IsOption("h") || IsOption("help"))
                {
                    options.ShowHelp = true;
                    return options;
                }

                if (IsOption("d") || IsOption("debug"))
                {
                    options.IsDryRun = true;
                    Debugger.Launch();
                }

                if (IsOption("DryRun") || IsOption("dr"))
                {
                    options.IsDryRun = true;
                }
            }

            options.BuildNumber = args[0];

            return options;
        }

        public void PrintHelp()
        {
            Console.WriteLine(@"TestFailureAnalyzer.exe <global-options> [command] <options>");
            Console.WriteLine();
            Console.WriteLine(@"Global options");
            Console.WriteLine(@"--------------");
            Console.WriteLine(@"  -h|-help             - Prints usage information");
            Console.WriteLine(@"  -DryRun|-dr          - Do not create new defects");
            Console.WriteLine();
        }
    }
}
