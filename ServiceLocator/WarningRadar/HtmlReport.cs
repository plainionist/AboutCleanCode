
namespace WarningRadar;

public class HtmlReport
{
    public void Generate(IReadOnlyCollection<CompilerAlert> alerts, TextWriter writer)
    {
        writer.WriteLine("<html>");
        writer.WriteLine("<body>");

        foreach (var group in alerts.GroupBy(x => x.Code))
        {
            writer.WriteLine($"  <h2>{group.Key}</h2>");
            writer.WriteLine("  <ul>");

            foreach (var file in group.Select(x => x.File).Distinct())
            {
                writer.WriteLine($"    <li>{file}</li>");
            }

            writer.WriteLine("  </ul>");
            writer.WriteLine();
        }

        writer.WriteLine("</body>");
        writer.WriteLine("</html>");
    }
}
