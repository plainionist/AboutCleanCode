
namespace TestFailureAnalyzer.Core.Notifications
{
    public interface IMailClient
    {
        void Send(Mail mail);
    }
}
