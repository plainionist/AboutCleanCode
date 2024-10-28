namespace HowToInCA.DataAccess.NuGet;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HowToInCA.Application.FeatureA;

public class NuGetClient : INuGetClient
{
    private static readonly HttpClient myClient = new();
    private const string NuGetApiBaseUrl = "https://api.nuget.org/v3/";

    public async Task<Result<Version, string>> GetLatestVersionAsync(string packageName)
    {
        Contract.RequiresNotNullNotEmpty(nameof(packageName));

        var response = await myClient.GetAsync($"{NuGetApiBaseUrl}flatcontainer/{packageName.ToLowerInvariant()}/index.json");
        if (!response.IsSuccessStatusCode)
        {
            return $"Failed to get package information. Status Code: {response.StatusCode}";
        }

        var content = await response.Content.ReadAsStringAsync();

        var jsonDoc = JsonDocument.Parse(content);

        if (!jsonDoc.RootElement.TryGetProperty("versions", out var versions))
        {
            return $"'versions' property not found in the response";
        }

        var version = versions.EnumerateArray()
            .Select(TryParseVersion)
            .Where(v => v != null)
            .OrderByDescending(v => v)
            .FirstOrDefault();

        return version != null ? version : $"Failed to parse version information";
    }

    private static Version? TryParseVersion(JsonElement element)
    {
        var versionString = element.GetString();
        if (versionString == null)
        {
            return null;
        }

        return Version.TryParse(versionString, out var version) ? version : null;
    }

    public async Task<IReadOnlyCollection<Result<TargetFramework, string>>> GetSupportedFrameworksAsync(string packageName, Version version)
    {
        Contract.RequiresNotNullNotEmpty(nameof(packageName));
        Contract.RequiresNotNullNotEmpty(nameof(version));

        var response = await myClient.GetAsync($"{NuGetApiBaseUrl}registration5-semver1/{packageName.ToLowerInvariant()}/{version}.json");
        if (!response.IsSuccessStatusCode)
        {
            return [$"Failed to get package version information. Status Code: {response.StatusCode}"];
        }

        var content = await response.Content.ReadAsStringAsync();
        var jsonDoc = JsonDocument.Parse(content);

        if (!jsonDoc.RootElement.TryGetProperty("catalogEntry", out JsonElement catalogEntry))
        {
            return [$"'catalogEntry' property not found in the response"];
        }

        response = await myClient.GetAsync(catalogEntry.GetString());
        if (!response.IsSuccessStatusCode)
        {
            return [$"Failed to get catalog entry information. Status Code: {response.StatusCode}"];
        }

        content = await response.Content.ReadAsStringAsync();
        jsonDoc = JsonDocument.Parse(content);

        if (!jsonDoc.RootElement.TryGetProperty("dependencyGroups", out var dependencyGroups))
        {
            return [$"'dependencyGroups' property not found in the response"];
        }

        return ParseTargetFrameworks(dependencyGroups).ToList();
    }

    private static IEnumerable<Result<TargetFramework, string>> ParseTargetFrameworks(JsonElement dependencyGroups)
    {
        foreach (var group in dependencyGroups.EnumerateArray())
        {
            if (group.TryGetProperty("targetFramework", out JsonElement targetFramework))
            {
                var framework = targetFramework.GetString();
                if (!string.IsNullOrEmpty(framework))
                {
                    yield return ParseTargetFramework(framework);
                }
            }
        }
    }

    private static Result<TargetFramework, string> ParseTargetFramework(string targetFramework)
    {
        var frameworks = new Dictionary<string, FrameworkType>()
        {
            { ".NETStandard", FrameworkType.NetStandard },
            { ".NETCoreApp", FrameworkType.NetCore },
            { ".NETFramework", FrameworkType.NetFramework },
            { "net", FrameworkType.NetFramework }
        };

        foreach (var (prefix, frameworkType) in frameworks)
        {
            if (targetFramework.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                var versionString = targetFramework.Replace(prefix, string.Empty);
                return Version.TryParse(versionString, out var version)
                    ? new TargetFramework(frameworkType, version)
                    : $"Invalid version format: {targetFramework}";
            }
        }

        return $"Unknown framework format: {targetFramework}";
    }
}
