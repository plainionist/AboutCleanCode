using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextAnalyzer;

public class WordsAnalyzer
{
    public IDictionary<string, int> CountWords(string text)
    {
        var words = ExtractAllWords(text);

        var relevantWords = words
            .Where(x => x.Length > 2)
            .ToList();

        var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var word in relevantWords)
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
        var matches = Regex.Matches(text, @"[a-zA-Z]+");

        foreach (Match match in matches)
        {
            yield return match.Value;
        }
    }
}
