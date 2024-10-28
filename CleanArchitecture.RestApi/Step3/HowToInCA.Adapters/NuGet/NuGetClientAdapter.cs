namespace HowToInCA.Adapters.NuGet;

using System.Threading.Tasks;
using HowToInCA.Application.FeatureA;

public class NuGetClientAdapter : INuGetClient
{
    private readonly INuGetApi myNuGetClient;
    private readonly NuGetResponseParser myParser;

    public NuGetClientAdapter(INuGetApi client)
    {
        Contract.RequiresNotNull(client);

        myNuGetClient = client;

        myParser = new NuGetResponseParser();
    }

    public async Task<Result<Version, string>> GetLatestVersionAsync(string packageName)
    {
        Contract.RequiresNotNullNotEmpty(nameof(packageName));

        var result = await myNuGetClient.QueryVersionsAsync(packageName);

        return result.Select(myParser.GetLatestVersion, e => e.Message);
    }

    public async Task<IReadOnlyCollection<Result<TargetFramework, string>>> GetSupportedFrameworksAsync(string packageName, Version version)
    {
        Contract.RequiresNotNullNotEmpty(nameof(packageName));
        Contract.RequiresNotNullNotEmpty(nameof(version));

        var response = await myNuGetClient.QueryRegistrationAsync(packageName, version);
        if (!response.IsSuccess)
        {
            return [response.Error.Message];
        }

        var result = myParser.GetCatalogUri(response.Value);
        if (!response.IsSuccess)
        {
            return [response.Error.Message];
        }

        response = await myNuGetClient.QueryCatalogAsync(result.Value);

        return response.Select(myParser.GetSupportedFrameworks, e => [e.Message]);
    }
}
