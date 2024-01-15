using System;
using System.Linq;

namespace TextAnalyzer.Tests;

internal class FakeWordSelectionStrategy : IWordSelectionStrategy
{
    public bool IsRelevant(string word) => word.All(Char.IsUpper);
}