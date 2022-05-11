using System.Collections.Generic;
using System.Data.SqlClient;
using TestFailureAnalyzer.Core.Tests;

namespace TestFailureAnalyzer.IO.TestDatabase
{
    public class TestDatabaseClient : ITestFailureDB
    {
        private const string ConnectionString = @"....";

        private readonly bool myIsDryRun;

        public TestDatabaseClient(bool isDryRun)
        {
            myIsDryRun = isDryRun;
        }

        public IReadOnlyCollection<TestFailure> GetTestFailures(string buildNumber)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                // TODO: querying DB goes here ...

                return new List<TestFailure>();
            }
        }

        public void UpdateFailure(TestFailure failure, int id)
        {
            if (myIsDryRun)
            {
                return;
            }

            using (var conn = new SqlConnection(ConnectionString))
            {
                // TODO: update of DB goes here ...
            }
        }
    }
}
