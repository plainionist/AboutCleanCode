using NUnit.Framework;

namespace TextAnalyzer.Tests;

[TestFixture]
public class WordsAnalyzerTests
{
    [Test]
    public void WordsCountedProperly()
    {
        var analyzer = new WordsAnalyzer();

        var response = analyzer.CountWords("word one, word two");

        Assert.That(response["one"], Is.EqualTo(1));
        Assert.That(response["two"], Is.EqualTo(1));
        Assert.That(response["word"], Is.EqualTo(2));
    }

    [Test]
    public void NumbersAreCutOffFromWords()
    {
        var analyzer = new WordsAnalyzer();

        var response = analyzer.CountWords("param1 and param2");

        Assert.That(response.Keys, Is.EquivalentTo(new[] { "param", "and" }));
        Assert.That(response["param"], Is.EqualTo(2));
        Assert.That(response["and"], Is.EqualTo(1));
    }

    [Test]
    public void WordsShorterThanTwoCharactersAreIgnored()
    {
        var analyzer = new WordsAnalyzer();

        var response = analyzer.CountWords("i am a developer");

        Assert.That(response.Keys, Is.EquivalentTo(new[] { "developer" }));
    }

    [Test]
    public void OnlyCamelCaseWordsAreCounted()
    {
        var analyzer = new WordsAnalyzer();

        var response = analyzer.CountWords("The WordAnalyzer component needs to detect CamelCase words.");

        Assert.That(response.Keys, Is.EquivalentTo(new[] { "WordAnalyzer", "CamelCase" }));
        Assert.That(response["WordAnalyzer"], Is.EqualTo(1));
        Assert.That(response["CamelCase"], Is.EqualTo(1));
    }

    [Test]
    public void CaseIsIgnored()
    {
        var analyzer = new WordsAnalyzer();

        var response = analyzer.CountWords("red Red RED");

        Assert.That(response["red"], Is.EqualTo(3));
    }

    [Test]
    public void NumbersAreIgnored()
    {
        var analyzer = new WordsAnalyzer();
        var response = analyzer.CountWords("The count is 123");

        Assert.That(response.ContainsKey("123"), Is.False);
    }
}
