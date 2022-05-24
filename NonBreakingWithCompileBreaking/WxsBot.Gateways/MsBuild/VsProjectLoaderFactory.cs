
using System.Xml.Linq;
using WxsBot.Entities;

namespace WxsBot.Gateways.MsBuild
{
    public class VsProjectLoaderFactory
    {
        public IVsProjectLoader Create()
        {
            return new VsProjectLoaderAdapter();
        }

        class VsProjectLoaderAdapter : IVsProjectLoader
        {
            public VsProject Load(string projectPath)
            {
                var doc = XElement.Load(projectPath);
                return doc.Attribute("Sdk") != null
                    ? new SdkStyleVsProjectLoader().Load(projectPath)
                    : new LegacyVsProjectLoader().Load(projectPath);
            }
        }
    }
}