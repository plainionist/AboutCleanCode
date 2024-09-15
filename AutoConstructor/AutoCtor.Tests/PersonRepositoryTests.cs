namespace AutoCtor.Tests;

public class PersonRepositoryTests
{
    [Test]
    public void InitializerIsCalled()
    {
        var logger = new FakeLogger();
        
        new PersonRepository(logger, "FakeConnectionString");

        Assert.That(logger.Messages, Contains.Item("Creating Person table on demand at 'FakeConnectionString' ..."));
    }
}

internal class FakeLogger : ILogger
{
    public List<string> Messages { get; } = [];

    public void Information(string msg)
    {
        Messages.Add(msg);
    }
}