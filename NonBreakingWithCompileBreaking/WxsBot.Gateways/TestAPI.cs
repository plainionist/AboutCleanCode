using WxsBot.Entities;
using WxsBot.Gateways.MsBuild;

namespace WxsBot.Gateways
{
    public static class TestApi
    {
        public static VsProject LoadVsProject(string path)
        {
            var factory = new VsProjectLoaderFactory();
            var loader = factory.Create();
            return loader.Load(path);
        }
    }
}