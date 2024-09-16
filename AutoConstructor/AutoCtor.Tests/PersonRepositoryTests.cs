
namespace AutoCtor.Tests;

public class PersonRepositoryTests
{
    [Test]
    public void GuardsGenerated()
    {
        Assert.Throws<ArgumentNullException>(() => new PersonRepository(null, null, null));
    }

    [Test]
    public void InitializerIsCalled()
    {
        var logger = new FakeLogger();
        var connectionFactory = new FakeConnectionFactory();
        var configuration = new Configuration();

        new PersonRepository(logger, connectionFactory, configuration);

        Assert.That(logger.Messages, Contains.Item("Initializing Person table"));
    }
}

internal class FakeConnectionFactory : ISqlConnectionFactory
{
    public FakeConnectionFactory()
    {
    }

    public IDisposable Create()
    {
        throw new NotImplementedException();
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