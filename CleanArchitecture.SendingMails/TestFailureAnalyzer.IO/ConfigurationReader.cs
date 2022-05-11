
using System.Collections.Generic;

namespace TestFailureAnalyzer.IO
{
    public class ConfigurationReader : IConfigurationReader
    {
        public IReadOnlyCollection<string> GetApplicationOperators()
        {
            return null;
        }

        public string GetMailSender()
        {
            return null;
        }
    }
}