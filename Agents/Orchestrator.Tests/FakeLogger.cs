using System.Collections.Generic;

namespace AboutCleanCode.Orchestrator.Tests;

class FakeLogger : ILogger
{
    public List<string> Messages { get; } = new();

    public void Debug(object owner, string message) =>
        Messages.Add($"{owner.GetType().FullName}|DEBUG|{message}");

    public void Error(object owner, string message) =>
        Messages.Add($"{owner.GetType().FullName}|ERROR|{message}");

    public void Info(object owner, string message) =>
        Messages.Add($"{owner.GetType().FullName}|INFO|{message}");

    public void Warning(object owner, string message) =>
        Messages.Add($"{owner.GetType().FullName}|WARNING|{message}");
}