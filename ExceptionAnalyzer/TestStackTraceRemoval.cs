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

        var exceptionLines1 = exception1.StackTrace.Split('\n');

        var formattedException1 = new StringBuilder();

        foreach (var line in exceptionLines1)
        {
            if (IsStackTraceLineFromTestClass(line))
            {
                continue;
            }

            formattedException1.AppendLine(line);
        }

        var formattedException2 = new StringBuilder();
        var exceptionLines2 = exception2.StackTrace.Split('\n');

        foreach (var line in exceptionLines2)
        {
            if (IsStackTraceLineFromTestClass(line))
            {
                continue;
            }

            formattedException2.AppendLine(line);
        }

        return formattedException1.ToString() == formattedException2.ToString();
    }

    private static bool IsStackTraceLineFromTestClass(string line) =>
        (Regex.IsMatch(line, @"Tests\.Company\..*")
            || Regex.IsMatch(line, @"Company\.Product\..*(Integration|System)Tests\..*"))
            && (Regex.IsMatch(line, @"Tests\.")
            || Regex.IsMatch(line, @"HzTests\."));
}
