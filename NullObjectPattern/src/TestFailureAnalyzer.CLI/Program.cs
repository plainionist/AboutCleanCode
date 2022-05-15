using System;
using TestFailureAnalyzer.Core;
using TestFailureAnalyzer.Core.Defects;
using TestFailureAnalyzer.Core.Notifications;
using TestFailureAnalyzer.Core.Tests;
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

                var mailClient = options.IsDryRun
                    ? (IMailClient)new NullMailClient()
                    : new MailClient(Environment.GetEnvironmentVariable("SMTP_SERVER"));
                var testDatabaseReader = new TestDatabaseReader();
                var testDatabaseWriter = options.IsDryRun
                    ? (ITestDatabaseWriter)new NullTestDataBaseWriter()
                    : new TestDatabaseWriter();
                var tfsClient = options.IsDryRun
                    ? (IDefectRepository)new TfsReadOnlyLoggingClient(new TfsClient(), Console.Out)
                    : new TfsClient();

                var processor = new TestFailureProcessor(
                    testDatabaseReader,
                    testDatabaseWriter,
                    tfsClient,
                    mailClient);

                processor.ProcessFailedTests(options.BuildNumber);

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
