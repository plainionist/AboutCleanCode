using System.Collections.Generic;
using TestFailureAnalyzer.Core.Tests;

namespace TestFailureAnalyzer.CLI
{
    internal class NullTestDataBaseWriter : ITestDatabaseWriter
    {
        public void UpdateFailure(TestFailure failure, int id)
        {
        }
    }
}