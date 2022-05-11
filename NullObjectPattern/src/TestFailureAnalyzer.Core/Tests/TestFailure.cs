using System;

namespace TestFailureAnalyzer.Core.Tests
{
    public class TestFailure
    {
        public string TestCaseName { get; set; }
        public Exception Exception { get; set; }
        public int DefectId { get; internal set; }
        public Uri TestOutputUrl { get; internal set; }
    }
}