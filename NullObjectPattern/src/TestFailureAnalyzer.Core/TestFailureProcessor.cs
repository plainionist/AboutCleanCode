using System;
using TestFailureAnalyzer.Core.Defects;
using TestFailureAnalyzer.Core.Notifications;
using TestFailureAnalyzer.Core.Tests;

namespace TestFailureAnalyzer.Core
{
    public class TestFailureProcessor
    {
        private readonly ITestDatabaseReader myTestDatabaseReader;
        private readonly ITestDatabaseWriter myTestDatabaseWriter;
        private readonly IMailClient myEmailClient;
        private readonly IDefectRepository myDefectRepository;

        public TestFailureProcessor(
            ITestDatabaseReader testDatabaseReader,
            ITestDatabaseWriter testDatabaseWriter,
            IDefectRepository defectRepository,
            IMailClient emailClient)
        {
            myTestDatabaseReader = testDatabaseReader;
            myTestDatabaseWriter = testDatabaseWriter;
            myDefectRepository = defectRepository;
            myEmailClient = emailClient;
        }

        public void ProcessFailedTests(string buildNumber)
        {
            try
            {
                var defectCount = 0;
                foreach (var failure in myTestDatabaseReader.GetTestFailures(buildNumber))
                {
                    // Processing of test failures like removing duplicates
                    // goes here ...

                    if (failure.DefectId == -1)
                    {
                        // update existing defect with new failure information

                        var existingDefect = myDefectRepository.Find(failure.DefectId);
                        existingDefect.Fields["Description"] = existingDefect.Fields["Description"] + Environment.NewLine + failure.TestCaseName;
                        existingDefect.Links.Add(failure.TestOutputUrl);

                        myDefectRepository.UpdateDefect(existingDefect);

                        continue;
                    }

                    defectCount++;

                    var input = CreateDefectInput(buildNumber, failure);

                    var defect = myDefectRepository.CreateDefect(input);

                    // remember that this kind of failure already got documented by a defect
                    myTestDatabaseWriter.UpdateFailure(failure, defect.Id);
                }

                var mail = CreateDefectsSuccessMail(buildNumber, defectCount);
                myEmailClient.Send(mail);
            }
            catch (Exception exception)
            {
                var mail = CreateProcessingFailedMail(buildNumber, exception);
                myEmailClient.Send(mail);

                throw;
            }
        }

        private DefectInput CreateDefectInput(string buildNumber, TestFailure failure) =>
            new DefectInput();

        private Mail CreateDefectsSuccessMail(string buildNumber, int defectCount) =>
            new Mail();

        private Mail CreateProcessingFailedMail(string buildNumber, Exception exception) =>
            new Mail();
    }
}
