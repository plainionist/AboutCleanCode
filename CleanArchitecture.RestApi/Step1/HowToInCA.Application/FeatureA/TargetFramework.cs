namespace HowToInCA.Application.FeatureA;

using System;

public enum FrameworkType
{
    NetFramework,
    NetCore,
    Net,
    NetStandard
}

public record TargetFramework(FrameworkType Type, Version Version)
{
    public override string ToString() => $"{Type} {Version}";
}
