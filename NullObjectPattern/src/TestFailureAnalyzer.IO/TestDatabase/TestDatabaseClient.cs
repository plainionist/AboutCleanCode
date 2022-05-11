using System.Collections.Generic;
using TestFailureAnalyzer.Core.Tests;

namespace TestFailureAnalyzer.IO.TestDatabase
{
    public class TestDatabaseClient : ITestFailureDB
    {
        private readonly bool myIsDryRun;

        public TestDatabaseClient(bool isDryRun)
        {
            myIsDryRun = isDryRun;
        }

        public IReadOnlyCollection<TestFailure> GetTestFailures(string buildNumber)
        {
                // TODO: querying DB goes here ...

                return new List<TestFailure>();
        }

        public void UpdateFailure(TestFailure failure, int id)
        {
            if (myIsDryRun)
            {
                return;
            }

            // TODO: update of DB goes here ...
        }
    }
}
