using System.Collections.Generic;

namespace TestFailureAnalyzer.Adapters
{
    public class HtmlMail
    {
        public string Sender { get; internal set; }
        public IReadOnlyCollection<string> Recipients { get; internal set; }
        public string Body { get; internal set; }
    }
}