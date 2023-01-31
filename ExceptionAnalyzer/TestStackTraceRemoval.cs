using System.Text;
using System.Text.RegularExpressions;

namespace ExceptionAnalyzer;

public class TestStackTraceRemoval
{
    public bool IsSame(string exceptionText1, string exceptionText2)
    {
        var parser = new ExceptionParser();
        var exception1 = parser.ExtractException(exceptionText1);
        var exception2 = parser.ExtractException(exceptionText2);

        if (exception1.Type != exception2.Type || exception1.Message != exception2.Message)
        {
            return false;
        }

        var stackTrace1 = exception1.StackTrace.Split('\n');

        var filteredStackTrace1 = new StringBuilder();

        foreach (var line in stackTrace1)
        {
            if (IsStackTraceLineFromTestClass(line))
            {
                continue;
            }

            filteredStackTrace1.AppendLine(line);
        }

        var filteredStackTrace2 = new StringBuilder();
        var stackTrace2 = exception2.StackTrace.Split('\n');

        foreach (var line in stackTrace2)
        {
            if (IsStackTraceLineFromTestClass(line))
            {
                continue;
            }

            filteredStackTrace2.AppendLine(line);
        }

        return filteredStackTrace1.ToString() == filteredStackTrace2.ToString();
    }

    private static bool IsStackTraceLineFromTestClass(string line) =>
        (Regex.IsMatch(line, @"Tests\.Company\..*")
            || Regex.IsMatch(line, @"Company\.Product\..*(Integration|System)Tests\..*"))
            && (Regex.IsMatch(line, @"Tests\.")
            || Regex.IsMatch(line, @"HzTests\."));
}
