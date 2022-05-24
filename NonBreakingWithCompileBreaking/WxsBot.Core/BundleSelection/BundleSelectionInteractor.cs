using System.Collections.Generic;
using System.IO;
using System.Linq;
using WxsBot.Entities;

namespace WxsBot.BundleSelection
{
       public class BundleSelectionInteractor
    {
        private readonly IWxsGenerationConfig myConfig;

        public BundleSelectionInteractor(IWxsGenerationConfig config)
        {
            myConfig = config;
        }

        public IReadOnlyCollection<Bundle> GetBundlesFromSpec(IReadOnlyCollection<string> specs)
        {
            var bundles = new List<Bundle>();

            foreach (var spec in specs)
            {
                var matched = Directory.GetDirectories(myConfig.SourceDirectory, spec, SearchOption.TopDirectoryOnly)
                    .Select(x => new Bundle(x))
                    .ToList();

                if (matched.Any(b => b.IsValid()))
                {
                    bundles.AddRange(matched);
                }
            }

            return bundles;
        }
    }
}
