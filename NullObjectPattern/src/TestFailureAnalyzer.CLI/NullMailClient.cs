using TestFailureAnalyzer.Core.Notifications;

namespace TestFailureAnalyzer.CLI
{
    internal class NullMailClient : IMailClient
    {
        public void Send(Mail mail)
        {
        }
    }
}