using System.ComponentModel.Design.Serialization;
using HowToInCA.Application.FeatureA;
using NUnit.Framework;

namespace HowToInCA.DataAccess.NuGet;

[TestFixture]
public class Tests
{
    [Test]
    public async Task GetSupportedFrameworksAsync()
    {
        var nuGetClient = new NuGetClient();

        var response = await nuGetClient.GetSupportedFrameworksAsync("Newtonsoft.Json", new Version(12, 0, 3));

        var supportedFrameworks = response
            .Select(x => x.Select(x => x, y => null!))
            .Where(x => x != null)
            .ToList();

        Assert.That(supportedFrameworks, Contains.Item(new TargetFramework(FrameworkType.NetStandard, new Version(2, 0))));
    }

    [Test]
    public async Task GetLatestVersionAsync()
    {
        var nuGetClient = new NuGetClient();

        var response = await nuGetClient.GetLatestVersionAsync("Newtonsoft.Json");

        Assert.That(response.IsSuccess, Is.True);
        Assert.That(response.Value.Major, Is.GreaterThanOrEqualTo(12));
    }
}