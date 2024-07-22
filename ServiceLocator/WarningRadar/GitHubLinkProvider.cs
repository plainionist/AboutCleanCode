namespace WarningRadar;

public class GitHubLinkProvider(Uri baseUri, string workspaceRoot) : ILinkProvider
{
    public Uri GetLink(string localFilePath)
    {
        var relativePath = localFilePath[workspaceRoot.Length..]
            .Replace("\\","/")
            .Trim('/');

        var builder = new UriBuilder(baseUri);
        builder.Path = baseUri.PathAndQuery.Trim('/') + "/blob/main/" + relativePath;

        return builder.Uri;
    }
}
