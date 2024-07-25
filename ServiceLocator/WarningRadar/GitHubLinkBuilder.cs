namespace WarningRadar;

public class GitHubLinkBuilder(Uri baseUri, string workspaceRoot) : ILinkBuilder
{
    public Uri BuildLink(string localFilePath)
    {
        var relativePath = localFilePath[workspaceRoot.Length..]
            .Replace("\\", "/")
            .Trim('/');

        var builder = new UriBuilder(baseUri)
        {
            Path = baseUri.PathAndQuery.Trim('/') + "/blob/main/" + relativePath
        };

        return builder.Uri;
    }
}
