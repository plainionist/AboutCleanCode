using System.Collections.Generic;

namespace TestFailureAnalyzer.Core.Notifications
{
    public class Mail
    {
        public Mail()
        {
            Recipients = new List<string>();
        }

        public string Sender { get; set; }
        public string Subject { get; set; }
        public IList<string> Recipients { get; private set; }
        public string HtmlBody { get; set; }
    }
}
