
using System;

namespace TestFailureAnalyzer
{
    public class MailNotificationInteractor
    {
        private IConfigurationReader myConfigReader;
        private ITestResultsRepository myTestResultsRepository;
        private IMailClient myMailClient;

        public MailNotificationInteractor(
            IConfigurationReader configReader,
            ITestResultsRepository testResultsRepository,
            IMailClient mailClient)
        {
            myConfigReader = configReader;
            myTestResultsRepository = testResultsRepository;
            myMailClient = mailClient;
        }

        public void NotifyInternalError(int testCaseId, Exception exception)
        {
            var mail = new InternalErrorMail
            {
                Sender = myConfigReader.GetMailSender(),
                Recipients = myConfigReader.GetApplicationOperators(),
                ErrorMessage = exception.Message,
                StackTrace = exception.StackTrace,
                TestCase = myTestResultsRepository.GetTestDetails(testCaseId).Name
            };
            myMailClient.Send(mail);
        }

        public void CheckMissingTestCases(string buildId)
        {
            // some more business logic goes here to detect
            // that there was a network issue

            var mail = new NetworkFailureMail
            {
                Sender = myConfigReader.GetMailSender(),
                Recipients = myConfigReader.GetApplicationOperators(),
                BuildId = buildId,
                //TestAgents = 
            };
            myMailClient.Send(mail);
       }
    }
}