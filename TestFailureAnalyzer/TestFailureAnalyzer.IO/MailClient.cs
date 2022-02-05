
namespace TestFailureAnalyzer.IO
{
    public class MailClient : IMailClient
    {
        public void Send(InternalErrorMail mail)
        {
        }

        public void Send(NetworkFailureMail mail)
        {
        }
    }
}