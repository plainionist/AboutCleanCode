using System.Text.RegularExpressions;

namespace WarningRadar.Tests
{
    public partial class HtmlReportTests
    {
        [Test]
        public void Generate_ShouldWriteHtmlReport_NoAlerts()
        {
            var writer = new StringWriter();
            var alerts = new List<CompilerAlert>();

            var report = new HtmlReport(writer);

            report.Generate(alerts);

            var html = writer.ToString();

            var expectedHtml = @"<html><body></body></html>";

            Assert.That(WhiteSpacePattern().Replace(html, ""), Is.EqualTo(WhiteSpacePattern().Replace(expectedHtml, "")));
        }

        [Test]
        public void Generate_ShouldWriteHtmlReport_Alerts()
        {
            var writer = new StringWriter();
            var alerts = new List<CompilerAlert>
                    {
                        new CompilerAlert("File1.cs", 12, "C1001"),
                        new CompilerAlert("File2.cs", 22, "C1001"),
                        new CompilerAlert("File3.cs", 57, "C2001"),
                        new CompilerAlert("File4.cs", 111, "C2001"),
                        new CompilerAlert("File5.cs", 124, "C2001")
                    };

            var report = new HtmlReport(writer);

            report.Generate(alerts);

            var html = writer.ToString();

            var expectedHtml = @"<html>
                        <body>
                        <h2>C1001</h2>
                        <ul>
                            <li>File1.cs</li>
                            <li>File2.cs</li>
                        </ul>
                        <h2>C2001</h2>
                        <ul>
                            <li>File3.cs</li>
                            <li>File4.cs</li>
                            <li>File5.cs</li>
                        </ul>
                        </body>
                        </html>";

            Assert.That(WhiteSpacePattern().Replace(html, ""), Is.EqualTo(WhiteSpacePattern().Replace(expectedHtml, "")));
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex WhiteSpacePattern();
    }
}
