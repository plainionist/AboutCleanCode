namespace TestFailureAnalyzer.Adapters
{
    public class MailClientAdapter : TestFailureAnalyzer.IMailClient
    {
        private IMailClient myMailClient;

        public MailClientAdapter(IMailClient mailClient)
        {
            myMailClient = mailClient;
        }

        public void Send(InternalErrorMail mail)
        {
            var htmlMail = new HtmlMail
            {
                Sender = mail.Sender,
                Recipients = mail.Recipients,
                Body = $"<html><body>{mail.ErrorMessage}</body></html>"
            };
            myMailClient.Send(htmlMail);
        }

        public void Send(NetworkFailureMail mail)
        {
        }
    }
}
