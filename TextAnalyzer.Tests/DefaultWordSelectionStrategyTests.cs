using NUnit.Framework;

namespace TextAnalyzer.Tests;

[TestFixture]
public class DefaultWordSelectionStrategyTests
{
    [Test]
    public void WordsShorterThanTwoCharactersAreIgnored()
    {
        var strategy = new DefaultWordSelectionStrategy();

        Assert.That(strategy.IsRelevant("i"), Is.False);
        Assert.That(strategy.IsRelevant("am"), Is.False);
        Assert.That(strategy.IsRelevant("a"), Is.False);
        Assert.That(strategy.IsRelevant("developer"), Is.True);
    }
}
