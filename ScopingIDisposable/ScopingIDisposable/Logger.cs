using System;

namespace ScopingIDisposable;

class ConsoleLogger : ILogger
{
    private string TimeStamp => DateTime.Now.ToString("yyyy-MM-DD HH:mm:ss.ffffff");

    public void Debug(object owner, string message) =>
        Console.WriteLine($"|{TimeStamp}|DEBUG|{owner.GetType().FullName}|{message}");

    public void Info(object owner, string message) =>
        Console.WriteLine($"|{TimeStamp}|INFO|{owner.GetType().FullName}|{message}");

    public void Warning(object owner, string message) =>
        Console.WriteLine($"|{TimeStamp}|WARNING|{owner.GetType().FullName}|{message}");

    public void Error(object owner, string message) =>
        Console.WriteLine($"|{TimeStamp}|ERROR|{owner.GetType().FullName}|{message}");

    public IDisposable Scope(object owner, string method) =>
        new LoggingScope(this, owner, method);

    private class LoggingScope : IDisposable
    {
        private readonly ConsoleLogger myLogger;
        private object myOwner;
        private string myMethod;

        public LoggingScope(ConsoleLogger logger, object owner, string method)
        {
            myLogger = logger;
            myOwner = owner;
            myMethod = method;

            myLogger.Debug(owner, $"Entering {method}");
        }

        public void Dispose()
        {
            myLogger.Debug(myOwner, $"Leaving {myMethod}");
        }
    }
}