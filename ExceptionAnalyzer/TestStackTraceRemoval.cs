using System.Text;
using System.Text.RegularExpressions;

namespace ExceptionAnalyzer;

public class TestStackTraceRemoval
{
    public bool IsSame(string data1, string data2)
    {
        var parser = new ExceptionParser();
        var exception1 = parser.ExtractException(data1);
        var exception2 = parser.ExtractException(data2);

        if (exception1.Type == exception2.Type && exception1.Message == exception2.Message)
        {
            var exceptionLines1 = exception1.StackTrace.Split('\n');

            var formattedException1 = new StringBuilder();

            foreach (var line in exceptionLines1)
            {
                if ((Regex.IsMatch(line, @"Tests\.Company\..*")
                    || Regex.IsMatch(line, @"Company\.Product\..*(Integration|System)Tests\..*"))
                    && (Regex.IsMatch(line, @"Tests\.")
                    || Regex.IsMatch(line, @"HzTests\.")))
                {
                    continue;
                }

                formattedException1.AppendLine(line);
            }

            var formattedException2 = new StringBuilder();
            var exceptionLines2 = exception2.StackTrace.Split('\n');

            foreach (var line in exceptionLines2)
            {
                if ((Regex.IsMatch(line, @"Tests\.Company\..*")
                    || Regex.IsMatch(line, @"Company\.Product\..*(Integration|System)Tests\..*"))
                    && (Regex.IsMatch(line, @"Tests\.")
                    || Regex.IsMatch(line, @"HzTests\.")))
                {
                    continue;
                }

                formattedException2.AppendLine(line);
            }

            if (formattedException1.ToString() == formattedException2.ToString())
            {
                return true;
            }
        }

        return false;
    }
}
