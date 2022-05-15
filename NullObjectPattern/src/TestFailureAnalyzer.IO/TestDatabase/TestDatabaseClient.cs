using System.Collections.Generic;
using TestFailureAnalyzer.Core.Tests;

namespace TestFailureAnalyzer.IO.TestDatabase
{
    public class TestDatabaseReader : ITestDatabaseReader
    {
        public IReadOnlyCollection<TestFailure> GetTestFailures(string buildNumber)
        {
                // TODO: querying DB goes here ...

                return new List<TestFailure>();
        }
    }

    public class TestDatabaseWriter : ITestDatabaseWriter
    {
        public void UpdateFailure(TestFailure failure, int id)
        {
            // TODO: update of DB goes here ...
        }
    }
}
