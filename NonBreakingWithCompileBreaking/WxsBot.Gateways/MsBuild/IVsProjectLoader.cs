using WxsBot.Entities;

namespace WxsBot.Gateways.MsBuild
{
    public interface IVsProjectLoader
    {
        VsProject Load(string projectPath);
    }
}
