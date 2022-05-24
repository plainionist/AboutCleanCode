using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WxsBot.Entities;
using WxsBot.Gateways;
using WxsBot.Gateways.MsBuild;

namespace WxsBot
{
    public enum Command
    {
        None,
        GenerateForBundles,
        ApplyGovernanceRules
    }

    public class Program
    {
        private string mySourceDirectory;
        private string myConfigDirectory;
        private IReadOnlyCollection<string> myBundles;
        private Command myCommand;

        public static int Main(string[] args)
        {
            var logger = new ConsoleLogger();

            try
            {
                return new Program().Run(logger, args);
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + ex.ToString());

                PrintHelp();

                return 1;
            }
        }

        public int Run(ILogger logger, IReadOnlyList<string> args)
        {
            if (args.Count == 0)
            {
                PrintHelp();
                return 0;
            }

            for (var i = 0; i < args.Count; ++i)
            {
                bool IsOption(string option) =>
                    args[i].Equals($"-{option}", StringComparison.OrdinalIgnoreCase);

                bool IsCommand(string longCmd, string shortCmd) =>
                    args[i].Equals(longCmd, StringComparison.OrdinalIgnoreCase) ||
                    args[i].Equals(shortCmd, StringComparison.OrdinalIgnoreCase);

                if (IsOption("h") || IsOption("help"))
                {
                    PrintHelp();
                    return 0;
                }

                if (IsCommand("GenerateForBundle", "gb"))
                {
                    myCommand = Command.GenerateForBundles;

                    i++;

                    Contract.Requires(i > args.Count, "Command 'GenerateForBundle' requires an argument");

                    myBundles = args[i].Split(',').ToList();
                }
                else if (IsCommand("Governance", "gov"))
                {
                    myCommand = Command.ApplyGovernanceRules;

                    i++;

                    Contract.Requires(i > args.Count, "Command 'Governance' requires an argument");

                    myBundles = args[i].Split(',').ToList();
                }
                else if (IsOption("sources"))
                {
                    i++;

                    Contract.Requires(i > args.Count, "Option '-sources' requires an argument");

                    mySourceDirectory = args[i];

                    Contract.Requires(Directory.Exists(mySourceDirectory), "Given source directory does not exist.");
                }
                else if (IsOption("config"))
                {
                    i++;

                    Contract.Requires(i == args.Count, $"Option '-config' requires an argument");

                    myConfigDirectory = args[i];

                    Contract.Requires(Directory.Exists(myConfigDirectory), $"Given Config directory does not exist.");
                }
                else
                {
                    throw new ArgumentException($"Unknown argument: '{args[i]}'");
                }
            }

            var config = LoadConfigurations(logger, mySourceDirectory, myConfigDirectory);

            var factory = new VsProjectLoaderFactory();

            if (myCommand == Command.GenerateForBundles)
            {
                var cmd = new GenerateForBundleCommand(config, new VisualStudioSolutionFacade(logger, factory.Create()));
                cmd.ProcessBundles(myBundles);

                return 0;
            }
            else if (myCommand == Command.ApplyGovernanceRules)
            {
                var cmd = new ApplyGovernanceRulesCommand(config, new VisualStudioSolutionFacade(logger, factory.Create()));
                cmd.ProcessBundles(myBundles);

                return 0;
            }

            throw new ArgumentException("Unknown command");
        }

        private static void PrintHelp()
        {
            // TODO: to be implemented
        }

        private static IWxsGenerationConfig LoadConfigurations(ILogger logger, string sourceDirectory, string configFolderPath)
        {
            // TODO: to be implemented
            return null;
        }
    }
}
