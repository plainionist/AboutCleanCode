using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TextAnalyzer;

public class WordsAnalyzer
{
    public IDictionary<string, int> CountWords(string text)
    {
        var words = ExtractAllWords(text);

        var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var word in words)
        {
            if (!wordCounts.TryGetValue(word, out int value))
            {
                value = 0;
            }
            wordCounts[word] = ++value;
        }

        return wordCounts;
    }

    private static IEnumerable<string> ExtractAllWords(string text)
    {
        var matches = Regex.Matches(text, @"\w+[^\s]*\w+|\w");

        foreach (Match match in matches)
        {
            yield return match.Value;
        }
    }
}