using System.Collections.Generic;
using WxsBot.BundleSelection;
using WxsBot.Entities;
using WxsBot.WxsGeneration;

namespace WxsBot.Gateways
{
    public class GenerateForBundleCommand
    {
        private readonly IWxsGenerationConfig myConfig;
        private readonly IVisualStudioSolutionLoader myVsSolutionFacade;

        public GenerateForBundleCommand(IWxsGenerationConfig config, IVisualStudioSolutionLoader solutionAnalyzer)
        {
            myConfig = config;
            myVsSolutionFacade = solutionAnalyzer;
        }

        public void ProcessBundles(IReadOnlyCollection<string> specs)
        {
            var bundleSelectionInteractor = new BundleSelectionInteractor(myConfig);
            var bundles = bundleSelectionInteractor.GetBundlesFromSpec(specs);

            var interactor = new WxsGenerationInteractor(myVsSolutionFacade, myConfig);

            var docs = interactor.Generate(bundles);

            // TODO: write docs to file system and add to source control
        }
    }
}
