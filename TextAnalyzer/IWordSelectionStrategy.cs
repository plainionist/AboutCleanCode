namespace TextAnalyzer;

public interface IWordSelectionStrategy
{
    bool IsRelevant(string word);
}
