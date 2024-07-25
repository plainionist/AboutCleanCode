
namespace WarningRadar.Tests;

public class FakeLinkBuilder(string baseUrl) : ILinkBuilder
{
    public Uri BuildLink(string localFilePath) =>
        new($"{baseUrl}{localFilePath}");
}
