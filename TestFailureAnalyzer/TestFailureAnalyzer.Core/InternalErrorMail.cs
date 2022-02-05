using System.Collections.Generic;

namespace TestFailureAnalyzer
{
    public class InternalErrorMail
    {
        public string Sender { get; init; }
        public IReadOnlyCollection<string> Recipients { get; init; }
        public string TestCase { get; init; }
        public string ErrorMessage { get; init; }
        public string StackTrace { get; set; }
    }
}