using System;
using TestFailureAnalyzer.Core.Defects;
using TestFailureAnalyzer.Core.Notifications;
using TestFailureAnalyzer.Core.Tests;

namespace TestFailureAnalyzer.Core
{
    public class TestFailureProcessor
    {
        private readonly ITestFailureDB myTestFailureDB;
        private readonly IMailClient myEmailClient;
        private readonly IDefectRepository myDefectRepository;

        public TestFailureProcessor(ITestFailureDB dwhClient, IDefectRepository defectRepository, IMailClient emailClient)
        {
            myTestFailureDB = dwhClient;
            myDefectRepository = defectRepository;
            myEmailClient = emailClient;
        }

        public void ProcessFailedTests(string buildNumber, bool isDryRunEnabled)
        {
            try
            {
                var defectCount = 0;
                foreach (var failure in myTestFailureDB.GetTestFailures(buildNumber))
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

                    if (isDryRunEnabled)
                    {
                        // report to the operator that we would create a new defect for this failure
                        Console.WriteLine($"Creating defect for {failure.TestCaseName}");
                        Console.WriteLine($"  {failure.Exception.Message}");
                    }

                    var input = CreateDefectInput(buildNumber, failure);

                    var defect = myDefectRepository.CreateDefect(input);

                    // remember that this kind of failure already got documented by a defect
                    myTestFailureDB.UpdateFailure(failure, defect.Id);
                }

                if (myEmailClient != null)
                {
                    var mail = CreateDefectsSuccessMail(buildNumber, defectCount);
                    myEmailClient.Send(mail);
                }
            }
            catch (Exception exception)
            {
                if (myEmailClient != null)
                {
                    var mail = CreateProcessingFailedMail(buildNumber, exception);
                    myEmailClient.Send(mail);
                }

                throw;
            }
        }

        private DefectInput CreateDefectInput(string buildNumber, TestFailure failure) =>
            throw new NotImplementedException();

        private Mail CreateDefectsSuccessMail(string buildNumber, int defectCount) =>
            throw new NotImplementedException();

        private Mail CreateProcessingFailedMail(string buildNumber, Exception exception) =>
            throw new NotImplementedException();
    }
}
