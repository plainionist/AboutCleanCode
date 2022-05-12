using System;
using TestFailureAnalyzer.Core;
using TestFailureAnalyzer.IO.Smtp;
using TestFailureAnalyzer.IO.TestDatabase;
using TestFailureAnalyzer.IO.Tfs;

namespace TestFailureAnalyzer.CLI
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            return new Program().Run(args);
        }

        public int Run(string[] args)
        {
            try
            {
                var cli = new CommandLineParser();
                var options = cli.Parse(args);

                if (options.ShowHelp)
                {
                    cli.PrintHelp();
                    return 0;
                }

                var mailClient = options.IsDryRun ? null : new MailClient(Environment.GetEnvironmentVariable("SMTP_SERVER"));
                var testDatabaseClient = new TestDatabaseClient(options.IsDryRun);
                var tfsClient = new TfsClient(options.IsDryRun);

                var processor = new TestFailureProcessor(testDatabaseClient, tfsClient, mailClient);

                processor.ProcessFailedTests(options.BuildNumber, options.IsDryRun);

                return 0;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine($"FATAL ERROR: {exception.Message}");

                return 1;
            }
        }
    }
}
