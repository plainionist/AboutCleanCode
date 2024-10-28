namespace HowToInCA.Application.FeatureA;

public interface INuGetClient
{
     Task<Result<Version, string>> GetLatestVersionAsync(string packageName);
     Task<IReadOnlyCollection<Result<TargetFramework, string>>> GetSupportedFrameworksAsync(string packageName, Version version);
}