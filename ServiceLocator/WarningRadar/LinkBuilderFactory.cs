namespace WarningRadar;

public class LinkBuilderFactory
{
    private static Uri myBaseUri;
    private static string myWorkspaceRoot;

    public static void Load(string configFile)
    {
        // TODO: implement
    }

    public static ILinkBuilder CreateBuilder()
    {
        return new GitHubLinkBuilder(myBaseUri, myWorkspaceRoot);
    }
}
