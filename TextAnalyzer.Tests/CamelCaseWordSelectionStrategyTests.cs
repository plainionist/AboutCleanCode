using NUnit.Framework;

namespace TextAnalyzer.Tests;

[TestFixture]
public class CamelCaseWordSelectionStrategyTests
{
    [Test]
    public void CamelCaseWordsAreRelevant()
    {
        var strategy = new CamelCaseWordSelectionStrategy();

        Assert.That(strategy.IsRelevant("WordsAnalyzer"), Is.True);
        Assert.That(strategy.IsRelevant("CamelCase"), Is.True);
    }
}
