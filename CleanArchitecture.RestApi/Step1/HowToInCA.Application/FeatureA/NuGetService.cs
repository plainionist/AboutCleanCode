

namespace HowToInCA.Application.FeatureA;

public class NuGetService
{
    private readonly INuGetClient myNuGetClient;

    public NuGetService(INuGetClient client)
    {
        Contract.RequiresNotNull(client);

        myNuGetClient = client;
    }

    public async Task<AnalysisResult> AnalyzeNuGetPackage(NuGetPackageReference package)
    {
        var result = new AnalysisResult(package);

        var currentFrameworks = await myNuGetClient.GetSupportedFrameworksAsync(package.Name, package.Version);
        foreach (var framework in currentFrameworks)
        {
            framework.Match(result.CurrentFrameworks.Add, result.Errors.Add);
        }

        var latestVersion = await myNuGetClient.GetLatestVersionAsync(package.Name);
        if (!latestVersion.IsSuccess)
        {
            result.Errors.Add(latestVersion.Error);
            return result;
        }

        result.LatestVersion = latestVersion.Value;

        var latestFrameworks = await myNuGetClient.GetSupportedFrameworksAsync(package.Name, latestVersion.Value);
        foreach (var framework in latestFrameworks)
        {
            framework.Match(result.LatestFrameworks.Add, result.Errors.Add);
        }

        return result;
    }
}
