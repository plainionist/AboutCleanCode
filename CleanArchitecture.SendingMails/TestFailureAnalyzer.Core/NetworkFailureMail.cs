using System.Collections.Generic;

namespace TestFailureAnalyzer
{
    public class NetworkFailureMail
    {
        public string Sender { get; set; }
        public IReadOnlyCollection<string> Recipients { get; set; }
        public string BuildId { get; set; }
        public IReadOnlyCollection<string> TestAgents { get; set; }
    }
}