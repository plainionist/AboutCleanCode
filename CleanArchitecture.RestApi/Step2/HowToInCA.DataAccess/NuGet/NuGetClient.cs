namespace HowToInCA.DataAccess.NuGet;

using System.Net.Http;
using System.Threading.Tasks;
using HowToInCA.Application.FeatureA;

public class NuGetClient : INuGetClient
{
    private const string NuGetApiBaseUrl = "https://api.nuget.org/v3/";

    private readonly HttpClient myClient = new();
    private readonly NuGetResponseParser myParser = new();

    public async Task<Result<Version, string>> GetLatestVersionAsync(string packageName)
    {
        Contract.RequiresNotNullNotEmpty(packageName);

        var response = await myClient.GetAsync($"{NuGetApiBaseUrl}flatcontainer/{packageName.ToLowerInvariant()}/index.json");
        if (!response.IsSuccessStatusCode)
        {
            return $"Failed to get package information. Status Code: {response.StatusCode}";
        }

        var content = await response.Content.ReadAsStringAsync();

        return myParser.GetLatestVersion(content);
    }

    public async Task<IReadOnlyCollection<Result<TargetFramework, string>>> GetSupportedFrameworksAsync(string packageName, Version version)
    {
        Contract.RequiresNotNullNotEmpty(packageName);
        Contract.RequiresNotNull(version);

        var response = await myClient.GetAsync($"{NuGetApiBaseUrl}registration5-semver1/{packageName.ToLowerInvariant()}/{version}.json");
        if (!response.IsSuccessStatusCode)
        {
            return [$"Failed to get package version information. Status Code: {response.StatusCode}"];
        }

        var content = await response.Content.ReadAsStringAsync();

        var contentUri = myParser.GetCatalogUri(content);
        if (!contentUri.IsSuccess)
        {
            return [contentUri.Error];
        }

        response = await myClient.GetAsync(contentUri.ToString());
        if (!response.IsSuccessStatusCode)
        {
            return [$"Failed to get catalog entry information. Status Code: {response.StatusCode}"];
        }

        content = await response.Content.ReadAsStringAsync();

        return myParser.GetSupportedFrameworks(content);
    }
}
