using System;
using System.Collections.Generic;

namespace TestFailureAnalyzer.Adapters
{
    public class TestAPI
    {
        public static HtmlMail SimulateInternalErrorOccurred(Exception exception)
        {
            var fakeMailClient = new FakeMailClient();
            var mailAdapter = new MailClientAdapter(fakeMailClient);

            var interactor = new MailNotificationInteractor(
                new FakeConfigReader(),
                new FakeTestDatabase(),
                mailAdapter);

            interactor.NotifyInternalError(42, exception);

            return fakeMailClient.Mail;
        }

        private class FakeMailClient : Adapters.IMailClient
        {
            public HtmlMail Mail { get; private set; }
            public void Send(HtmlMail mail)
            {
                Mail = mail;
            }
        }

        private class FakeConfigReader : IConfigurationReader
        {
            IReadOnlyCollection<string> IConfigurationReader.GetApplicationOperators()
            {
                throw new System.NotImplementedException();
            }

            string IConfigurationReader.GetMailSender()
            {
                throw new System.NotImplementedException();
            }
        }

        private class FakeTestDatabase : ITestResultsRepository
        {
            public TestDetails GetTestDetails(int testCaseId)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
