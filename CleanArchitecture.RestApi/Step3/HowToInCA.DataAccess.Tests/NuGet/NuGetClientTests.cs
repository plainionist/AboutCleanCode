namespace HowToInCA.DataAccess.NuGet.Tests;

using HowToInCA.Adapters.NuGet;
using HowToInCA.Application.FeatureA;
using NUnit.Framework;

[TestFixture]
public class NuGetClientTests
{
    [Test]
    public async Task GetLatestVersionAsync()
    {
        var nuGetClient = new NuGetClient();
        var parser = new NuGetResponseParser();

        var response = await nuGetClient.QueryVersionsAsync("Newtonsoft.Json");
        var result = response.Select(parser.GetLatestVersion, e => e.Message);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Major, Is.GreaterThanOrEqualTo(12));
    }

    [Test]
    public async Task GetSupportedFrameworksAsync()
    {
        var nuGetClient = new NuGetClient();
        var parser = new NuGetResponseParser();

        var response = await nuGetClient.QueryRegistrationAsync("Newtonsoft.Json", new Version(12, 0, 3));
        var catalogUri = response.Select(parser.GetCatalogUri, e => e.Message);

        Assert.That(catalogUri.IsSuccess, Is.True);

        response = await nuGetClient.QueryCatalogAsync(catalogUri.Value);
        var result = response.Select(parser.GetSupportedFrameworks, e => [e.Message]);

        var supportedFrameworks = result
            .Select(x => x.Select(x => x, y => null!))
            .Where(x => x != null)
            .ToList();

        Assert.That(supportedFrameworks, Contains.Item(new TargetFramework(FrameworkType.NetStandard, new Version(2, 0))));
    }
}