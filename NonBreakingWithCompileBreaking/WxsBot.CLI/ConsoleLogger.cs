using System;

namespace WxsBot
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Success,
        None
    }
    
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void Warning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        public void Success(string message)
        {
            Log(LogLevel.Success, message);
        }

        private void Log(LogLevel level, string message)
        {
            Write(level, message);
        }

        public void EmptyLine()
        {
            Write(LogLevel.None, "");
        }

        public void Write(LogLevel level, string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = GetColor(level);

            Console.WriteLine(message);

            Console.ForegroundColor = oldColor;
        }

        private static ConsoleColor GetColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Info:
                    return ConsoleColor.White;
                case LogLevel.Warning:
                    return ConsoleColor.Yellow;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                case LogLevel.Success:
                    return ConsoleColor.DarkGreen;
                default:
                    return ConsoleColor.Gray;
            }
        }

    }
}
