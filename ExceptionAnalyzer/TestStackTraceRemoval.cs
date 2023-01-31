using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExceptionAnalyzer;

class TestClassInvariantComparer
{
    public bool IsSame(ExceptionInfo exception1, ExceptionInfo exception2)
    {
        if (exception1.Type != exception2.Type || exception1.Message != exception2.Message)
        {
            return false;
        }

        var stackTrace1 = exception1.StackTrace.Split('\n');

        var filteredStackTrace1 = stackTrace1
            .Where(x => !IsStackTraceLineFromTestClass(x))
            .ToList();

        var stackTrace2 = exception2.StackTrace.Split('\n');

        var filteredStackTrace2 = stackTrace2
            .Where(x => !IsStackTraceLineFromTestClass(x))
            .ToList();

        return !filteredStackTrace1.Except(filteredStackTrace2).Any();
    }

    private static bool IsStackTraceLineFromTestClass(string line) =>
        (Regex.IsMatch(line, @"Tests\.Company\..*")
            || Regex.IsMatch(line, @"Company\.Product\..*(Integration|System)Tests\..*"))
            && (Regex.IsMatch(line, @"Tests\.")
            || Regex.IsMatch(line, @"HzTests\."));
}
