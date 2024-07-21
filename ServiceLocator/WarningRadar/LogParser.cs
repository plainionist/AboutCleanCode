using System.Text.RegularExpressions;

namespace WarningRadar;

public record CompilerAlert(string File, int LineNumber, string Code);

public partial class LogParser
{
    private static readonly Regex myAlertPattern = AlertPattern();

    public IReadOnlyCollection<CompilerAlert> Parse(TextReader reader)
    {
        var content = reader.ReadToEnd();

        var errorMatches = myAlertPattern.Matches(content);

        return errorMatches.OfType<Match>()
            .Select(match => new CompilerAlert(
                File: match.Groups["file"].Value,
                LineNumber: int.Parse(match.Groups["line"].Value),
                Code: match.Groups["code"].Value))
            .ToList(); ;
    }

    [GeneratedRegex(@"(?<file>[^\(\s]+)\((?<line>\d+),\d+\):\s+(error|warning)\s+(?<code>[A-Za-z]+\d+)")]
    private static partial Regex AlertPattern();
}
