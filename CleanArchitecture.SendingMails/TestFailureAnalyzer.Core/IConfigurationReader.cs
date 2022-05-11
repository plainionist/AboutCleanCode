using System.Collections.Generic;

namespace TestFailureAnalyzer
{
    public interface IConfigurationReader
    {
        string GetMailSender();
        IReadOnlyCollection<string> GetApplicationOperators();
    }
}