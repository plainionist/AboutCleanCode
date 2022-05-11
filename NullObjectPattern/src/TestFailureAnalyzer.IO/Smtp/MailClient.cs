using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using TestFailureAnalyzer.Core.Notifications;

namespace TestFailureAnalyzer.IO.Smtp
{
    public class MailClient : IMailClient
    {
        private string mySmtpServer;

        public MailClient(string smtpServer)
        {
            mySmtpServer = smtpServer;
        }

        public void Send(Mail mail)
        {
            if (mail.Sender == null)
            {
                throw new ArgumentException("Sender missing");
            }
            if (!mail.Recipients.Any())
            {
                throw new ArgumentException("Recipients missing");
            }

            var message = new MailMessage
            {
                From = new MailAddress(mail.Sender),
                Subject = mail.Subject,
                IsBodyHtml = true,
                Body = mail.HtmlBody
            };

            foreach (var recipient in mail.Recipients)
            {
                message.To.Add(recipient);
            }

            var smtpClient = new SmtpClient
            {
                Host = mySmtpServer,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
            };
            smtpClient.Send(message);
        }
    }
}
