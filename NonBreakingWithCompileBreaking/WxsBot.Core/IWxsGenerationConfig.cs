using System;

namespace WxsBot.Entities
{
    public interface IWxsGenerationConfig
    {
        Version WxsVersion { get; }

        string SourceDirectory { get; }
    }
}
