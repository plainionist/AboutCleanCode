namespace WarningRadar.Tests;

public class LogParserTests
{
    [Test]
    public void Parse_ShouldReturnEmptyCollection_WhenReaderIsEmpty()
    {
        var reader = new StringReader(string.Empty);
        var parser = new LogParser();

        var result = parser.Parse(reader);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Parse_ShouldReturnCompilerAlerts_WhenReaderContainsAlerts()
    {
        var reader = new StringReader(@"C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz.ActorsHost\Program.cs(53,13): error CA1031: Modify 'Main' to catch a more specific allowed exception type, or rethrow the exception (https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca1031) [C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz.ActorsHost\Plainion.GraphViz.ActorsHost.csproj]\r\n");
        var parser = new LogParser();

        var result = parser.Parse(reader).ToList();

        Assert.That(result, Has.Count.EqualTo(1));

        Assert.That(result[0].File, Is.EqualTo(@"C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz.ActorsHost\Program.cs"));
        Assert.That(result[0].LineNumber, Is.EqualTo(53));
        Assert.That(result[0].Code, Is.EqualTo("CA1031"));
    }

    [Test]
    public void Parse_ShouldReturnMultipleCompilerAlerts_WhenReaderContainsMultipleAlerts()
    {
        var reader = new StringReader(@"
            C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz\Dot\DotPlainReader.cs(105,23): error CA2201: Exception type System.Exception is not sufficiently specific (https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca2201) [C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz\Plainion.GraphViz_2est5eci_wpftmp.csproj]
            C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz\Dot\DotWriter.cs(42,23): error CA1001: Type 'WriteAction' owns disposable field(s) 'myWriter' but is not disposable (https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca1001) [C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz\Plainion.GraphViz_2est5eci_wpftmp.csproj]
            ");
        var parser = new LogParser();

        var result = parser.Parse(reader).ToList();

        Assert.That(result, Has.Count.EqualTo(2));

        Assert.That(result[0].File, Is.EqualTo(@"C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz\Dot\DotPlainReader.cs"));
        Assert.That(result[0].LineNumber, Is.EqualTo(105));
        Assert.That(result[0].Code, Is.EqualTo("CA2201"));

        Assert.That(result[1].File, Is.EqualTo(@"C:\ws\plainionist\Plainion.GraphViz\src\Plainion.GraphViz\Dot\DotWriter.cs"));
        Assert.That(result[1].LineNumber, Is.EqualTo(42));
        Assert.That(result[1].Code, Is.EqualTo("CA1001"));
    }
}
