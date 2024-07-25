using System.Text.RegularExpressions;

namespace WarningRadar.Tests
{
    public partial class HtmlReportTests
    {
        [Test]
        public void Generate_ShouldWriteHtmlReport_Alerts()
        {
            var alerts = new List<CompilerAlert>
                    {
                        new("File1.cs", 12, "C1001"),
                        new("Dir1/File2.cs", 22, "C3201"),
                        new("Dir2/File3.cs", 111, "C3201"),
                    };

            var writer = new StringWriter();
            new HtmlReport(new FakeLinkBuilder("http://dummy/")).Generate(alerts, writer);
            var html = writer.ToString();

            var expectedHtml = @"<html>
                        <body>
                        <h2>C1001</h2>
                        <ul>
                            <li><a href=""http://dummy/File1.cs"">File1.cs</a></li>
                        </ul>
                        <h2>C3201</h2>
                        <ul>
                            <li><a href=""http://dummy/Dir1/File2.cs"">Dir1/File2.cs</a></li>
                            <li><a href=""http://dummy/Dir2/File3.cs"">Dir2/File3.cs</a></li>
                        </ul>
                        </body>
                        </html>";

            Assert.That(WhiteSpacePattern().Replace(html, ""), Is.EqualTo(WhiteSpacePattern().Replace(expectedHtml, "")));
        }

        [Test]
        public void Generate_ShouldWriteHtmlReport_NoAlerts()
        {
            var alerts = new List<CompilerAlert>();

            var writer = new StringWriter();
            new HtmlReport(null).Generate(alerts, writer);
            var html = writer.ToString();

            var expectedHtml = @"<html><body></body></html>";

            Assert.That(WhiteSpacePattern().Replace(html, ""), Is.EqualTo(WhiteSpacePattern().Replace(expectedHtml, "")));
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex WhiteSpacePattern();
    }
}
