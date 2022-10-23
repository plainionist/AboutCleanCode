using System;
using AboutCleanCode;

internal class ConsoleLogger : ILogger
{
    public void Debug(object owner, string message) =>
        Console.WriteLine($"{owner.GetType().Name}|DEBUG|{message}");

    public void Error(object owner, string message) =>
        Console.WriteLine($"{owner.GetType().Name}|ERROR|{message}");

    public void Info(object owner, string message) =>
        Console.WriteLine($"{owner.GetType().Name}|INFO|{message}");

    public void Warning(object owner, string message) =>
        Console.WriteLine($"{owner.GetType().Name}|WARNING|{message}");
}