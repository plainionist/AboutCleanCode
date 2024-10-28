namespace HowToInCA.DataAccess.NuGet;

using System.Net.Http;
using System.Threading.Tasks;
using HowToInCA.Adapters.NuGet;

public class NuGetClient : INuGetApi
{
    private const string NuGetApiBaseUrl = "https://api.nuget.org/v3/";

    private readonly HttpClient myClient = new();

    public async Task<Result<string, Exception>> QueryVersionsAsync(string packageName)
    {
        Contract.RequiresNotNullNotEmpty(packageName);

        var response = await myClient.GetAsync($"{NuGetApiBaseUrl}flatcontainer/{packageName.ToLowerInvariant()}/index.json");
        if (!response.IsSuccessStatusCode)
        {
            return new Exception($"Failed to get package information. Status Code: {response.StatusCode}");
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<Result<string, Exception>> QueryRegistrationAsync(string packageName, Version version)
    {
        Contract.RequiresNotNullNotEmpty(packageName);
        Contract.RequiresNotNull(version);

        var response = await myClient.GetAsync($"{NuGetApiBaseUrl}registration5-semver1/{packageName.ToLowerInvariant()}/{version}.json");
        if (!response.IsSuccessStatusCode)
        {
            return new Exception($"Failed to get package version information. Status Code: {response.StatusCode}");
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<Result<string, Exception>> QueryCatalogAsync(Uri uri)
    {
        Contract.RequiresNotNull(uri);

        var response = await myClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return new Exception($"Failed to get catalog entry information. Status Code: {response.StatusCode}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}
