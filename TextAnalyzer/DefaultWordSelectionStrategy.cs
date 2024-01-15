namespace TextAnalyzer;

public class DefaultWordSelectionStrategy
{
    public bool IsRelevant(string word) => word.Length > 2;
}
