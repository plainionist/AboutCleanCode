namespace AboutCleanCode;

public interface ILogger
{
    void Debug(object owner, string message);
    void Info(object owner, string message);
    void Warning(object owner, string message);
    void Error(object owner, string message);
}
