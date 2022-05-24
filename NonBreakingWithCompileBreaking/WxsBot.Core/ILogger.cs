namespace WxsBot
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Success(string message);
        void EmptyLine();
    }
}
