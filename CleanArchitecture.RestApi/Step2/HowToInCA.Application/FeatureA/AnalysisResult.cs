namespace HowToInCA.Application.FeatureA;

public class AnalysisResult(NuGetPackageReference package)
{
    public NuGetPackageReference Package { get; } = package;

    public List<TargetFramework> CurrentFrameworks { get; } = [];
    public List<TargetFramework> LatestFrameworks { get; } = [];
    public Version? LatestVersion { get; set; }

    public List<string> Errors { get; } = [];
}