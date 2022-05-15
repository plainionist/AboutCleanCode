using System.Collections.Generic;

namespace TestFailureAnalyzer.Core.Tests
{
    public interface ITestDatabaseReader
    {
        IReadOnlyCollection<TestFailure> GetTestFailures(string buildNumber);
    }

    public interface ITestDatabaseWriter
    {
        void UpdateFailure(TestFailure failure, int id);
    }
}
