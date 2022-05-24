using System.Collections.Generic;
using System.Linq;
using WxsBot.Entities;

namespace WxsBot.WxsGeneration
{
    public class WxsGenerationInteractor
    {
        private readonly IWxsGenerationConfig myConfig;
        private readonly IVisualStudioSolutionLoader myVsSolutionFacade;

        public WxsGenerationInteractor(IVisualStudioSolutionLoader vsSolutionFacade, IWxsGenerationConfig config)
        {
            myConfig = config;
            myVsSolutionFacade = vsSolutionFacade;
        }

        public IReadOnlyCollection<WxsDocument> Generate(IReadOnlyCollection<Bundle> bundles) =>
            bundles.SelectMany(Generate).ToList();

        private IReadOnlyCollection<WxsDocument> Generate(Bundle bundle)
        {
            var documents = new List<WxsDocument>();

            foreach (var project in myVsSolutionFacade.Load(myConfig.SourceDirectory, bundle.Name).Projects)
            {
                var doc = new WxsDocument("" /* TODO: location*/ );
                doc.Add(project.Assembly);

                foreach (var contentFile in project.ContentFiles)
                {
                    doc.Add(contentFile);
                }

                documents.Add(doc);
            }

            return documents;
        }
    }
}
