namespace WarningRadar.Tests;

public class GitHubLinkProviderTests
{
    private static readonly Uri BaseUri = new("https://github.com/plainionist/Plainion.GraphViz/");
    private static readonly string WorkspaceRoot = @"C:\ws\plainionist\Plainion.GraphViz";

    [Test]
    public void GetLink_ValidLocalFilePath_ReturnsCorrectGitHubLink()
    {
        var linkProvider = new GitHubLinkProvider(BaseUri, WorkspaceRoot);

        var actualLink = linkProvider.GetLink(@"C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz.ActorsHost\Program.cs");

        Assert.That(actualLink, Is.EqualTo(new Uri("https://github.com/plainionist/Plainion.GraphViz/blob/main/src/Plainion.GraphViz.ActorsHost/Program.cs")));
    }
}
