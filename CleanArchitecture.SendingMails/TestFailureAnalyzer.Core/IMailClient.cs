namespace TestFailureAnalyzer
{
    public interface IMailClient
    {
        void Send(InternalErrorMail mail);
        void Send(NetworkFailureMail mail);
    }
}