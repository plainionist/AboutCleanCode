using System.Collections.Generic;

namespace TestFailureAnalyzer.Core.Tests
{
    public interface ITestDatabase
    {
        IReadOnlyCollection<TestFailure> GetTestFailures(string buildNumber);
        void UpdateFailure(TestFailure failure, int id);
    }
}
