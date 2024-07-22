namespace WarningRadar;

public class LinkProviderFactory
{
    private static Uri myBaseUri;
    private static string myWorkspaceRoot;

    public static void Load(string configFile)
    {
        // TODO: implement
    }

    public static ILinkProvider CreateProvider()
    {
        return new GitHubLinkProvider(myBaseUri, myWorkspaceRoot);
    }
}
