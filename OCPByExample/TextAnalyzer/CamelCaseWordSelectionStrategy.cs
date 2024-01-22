using System.Text.RegularExpressions;

namespace TextAnalyzer;

public class CamelCaseWordSelectionStrategy : IWordSelectionStrategy
{
    public bool IsRelevant(string word) => Regex.IsMatch(word,"([A-Z][a-z0-9]+)+");
}
