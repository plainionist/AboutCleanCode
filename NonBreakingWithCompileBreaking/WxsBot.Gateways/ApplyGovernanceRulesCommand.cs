using System.Collections.Generic;
using WxsBot.Entities;

namespace WxsBot.Gateways
{
    public class ApplyGovernanceRulesCommand
    {
        private readonly IWxsGenerationConfig myConfig;
        private readonly IVisualStudioSolutionLoader myVsSolutionFacade;

        public ApplyGovernanceRulesCommand(IWxsGenerationConfig config, IVisualStudioSolutionLoader solutionAnalyzer)
        {
            myConfig = config;
            myVsSolutionFacade = solutionAnalyzer;
        }

        public void ProcessBundles(IReadOnlyCollection<string> specs)
        {
            // TODO: to be implemented
        }
    }
}
