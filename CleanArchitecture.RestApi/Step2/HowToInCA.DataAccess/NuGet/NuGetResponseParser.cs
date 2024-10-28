namespace HowToInCA.DataAccess.NuGet;

using System.Text.Json;
using HowToInCA.Application.FeatureA;

public class NuGetResponseParser
{
    public Result<Version, string> GetLatestVersion(string responseContent)
    {
        Contract.RequiresNotNullNotEmpty(responseContent);

        var jsonDoc = JsonDocument.Parse(responseContent);

        File.WriteAllText(@"c:\temp\1.json", JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true }));

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

    public Result<Uri, string> GetCatalogUri(string responseContent)
    {
        Contract.RequiresNotNullNotEmpty(responseContent);

        var jsonDoc = JsonDocument.Parse(responseContent);

        File.WriteAllText(@"c:\temp\2.json", JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true }));

        if (!jsonDoc.RootElement.TryGetProperty("catalogEntry", out JsonElement catalogEntry))
        {
            return $"'catalogEntry' property not found in the response";
        }

        var catalogUri = catalogEntry.GetString();

        return string.IsNullOrEmpty(catalogUri)
            ? $"Failed to parse catalog entry information"
            : new Uri(catalogUri);
    }

    public IReadOnlyCollection<Result<TargetFramework, string>> GetSupportedFrameworks(string responseContent)
    {
        var jsonDoc = JsonDocument.Parse(responseContent);

        File.WriteAllText(@"c:\temp\3.json", JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true }));

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
