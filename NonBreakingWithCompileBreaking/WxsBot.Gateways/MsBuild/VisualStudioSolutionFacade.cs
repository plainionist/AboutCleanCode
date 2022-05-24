
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WxsBot.Entities;

namespace WxsBot.Gateways.MsBuild
{
    public class VisualStudioSolutionFacade : IVisualStudioSolutionLoader
    {
        private ILogger myLogger;
        private IVsProjectLoader myProjectLoader;

        public VisualStudioSolutionFacade(ILogger logger, IVsProjectLoader projectLoader)
        {
            myLogger = logger;
            myProjectLoader = projectLoader;
        }

        public VsSolution Load(string sourceDirectory, string bundle)
        {
            var slnPath = Path.Combine(sourceDirectory, bundle, bundle + ".sln");


            if (!File.Exists(slnPath))
            {
                throw new ArgumentException($"Bundle solution not found for bundle '{bundle}'");
            }

            // TODO: get projects from solution file
            var projects = Directory.EnumerateDirectories(Path.Combine(sourceDirectory, bundle), "*.csproj", SearchOption.AllDirectories)
                .Select(myProjectLoader.Load)
                .ToList();

            return new VsSolution(slnPath, projects);
        }

        public static void Create(IEnumerable<string> projects, string solutionPath)
        {
            // TODO: to be implemented
        }
    }
}
