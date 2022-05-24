
using WxsBot.Entities;

namespace WxsBot
{
    public interface IVisualStudioSolutionLoader
    {
        VsSolution Load(string sourceDirectory, string bundle);
    }
}
