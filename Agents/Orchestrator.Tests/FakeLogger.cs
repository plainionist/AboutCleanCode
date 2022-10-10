using System.Collections.Concurrent;

namespace AboutCleanCode.Orchestrator.Tests;

class FakeLogger : ILogger
{
    public ConcurrentQueue<string> Messages { get; } = new();

    public void Debug(object owner, string message) =>
        Messages.Enqueue($"{owner.GetType().FullName}|DEBUG|{message}");

    public void Error(object owner, string message) =>
        Messages.Enqueue($"{owner.GetType().FullName}|ERROR|{message}");

    public void Info(object owner, string message) =>
        Messages.Enqueue($"{owner.GetType().FullName}|INFO|{message}");

    public void Warning(object owner, string message) =>
        Messages.Enqueue($"{owner.GetType().FullName}|WARNING|{message}");
}