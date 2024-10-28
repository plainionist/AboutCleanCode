namespace HowToInCA.Adapters.NuGet;

public interface INuGetApi
{
    Task<Result<string, Exception>> QueryVersionsAsync(string packageName);
    Task<Result<string, Exception>> QueryRegistrationAsync(string packageName, Version version);
    Task<Result<string, Exception>> QueryCatalogAsync(Uri catalogUri);
}