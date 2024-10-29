namespace HowToInCA.AcceptanceTests;

using NUnit.Framework;
using HowToInCA.Application.FeatureA;
using HowToInCA.Adapters.NuGet;

[TestFixture]
public class FeatureATests
{
    [Test]
    public async Task Scenario_1()
    {
        var nuGetClient = new NuGetClientAdapter(new FakeNuGetClient());
        var service = new NuGetService(nuGetClient);

        // GIVEN ...
        var package = new NuGetPackageReference("Newtonsoft.Json", new Version(12, 0, 3));

        // WHEN ...
        var report = await service.AnalyzeNuGetPackage(package);

        // THEN ...
    }
}
