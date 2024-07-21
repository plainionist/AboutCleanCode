
namespace WarningRadar;

public class HtmlReport
{
    private readonly TextWriter myWriter;

    public HtmlReport(TextWriter writer)
    {
        myWriter = writer;
    }

    public void Generate(IReadOnlyCollection<CompilerAlert> alerts)
    {
        myWriter.WriteLine("<html>");
        myWriter.WriteLine("<body>");

        foreach (var group in alerts.GroupBy(x => x.Code))
        {
            myWriter.WriteLine($"  <h2>{group.Key}</h2>");
            myWriter.WriteLine("  <ul>");

            foreach (var file in group.Select(x => x.File).Distinct())
            {
                myWriter.WriteLine($"    <li>{file}</li>");
            }

            myWriter.WriteLine("  </ul>");
            myWriter.WriteLine();
        }

        myWriter.WriteLine("</body>");
        myWriter.WriteLine("</html>");
    }
}
