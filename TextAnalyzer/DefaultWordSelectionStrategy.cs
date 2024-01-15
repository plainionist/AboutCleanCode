namespace TextAnalyzer;

public class DefaultWordSelectionStrategy : IWordSelectionStrategy
{
    public bool IsRelevant(string word) => word.Length > 2;
}
