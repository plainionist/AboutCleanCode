using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExceptionAnalyzer;

class ExceptionParser
{
    public ExceptionInfo ExtractException(string exceptionText)
    {
        var lines = ParseText(exceptionText);

        var (exceptionType, exceptionMessage) = ParseTypeAndMessage(lines);
        var stackTrace = GetStackTrace(lines);

        return new()
        {
            Type = exceptionType,
            Message = exceptionMessage,
            StackTrace = stackTrace
        };
    }

    private static (string, string) ParseTypeAndMessage(List<string> lines)
    {
        var exceptionType = lines[0].Split(':').First();
        var exceptionMessage = lines[0][(lines[0].IndexOf(':') + 1)..];
        return (exceptionType, exceptionMessage);
    }

    private static List<string> ParseText(string exceptionText)
    {
        return exceptionText.Split(Environment.NewLine)
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();
    }

    private static List<StackTraceLine> GetStackTrace(IEnumerable<string> lines)
    {
        return lines
            .Skip(1)
            .Select(x => x.Trim())
            .Where(x => x.StartsWith("at "))
            .Select(x => x[3..])
            .Select(x => CreateStackTraceLine(x))
            .ToList();
    }

    public static StackTraceLine CreateStackTraceLine(string line)
    {
        var apiWithoutParams = line[..line.IndexOf('(')];
        var parameters = line[line.IndexOf('(')..].Trim('(', ')');
        var tokens = apiWithoutParams.Split('.');
        return new StackTraceLine(
            line,
            IsStackTraceLineFromTestClass(line),
            NameSpace: string.Join(".", tokens.Take(tokens.Length - 2)),
            ClassName: tokens[^2],
            Method: tokens[^1],
            Parameters: parameters);
    }

    private static bool IsStackTraceLineFromTestClass(string line) =>
        (Regex.IsMatch(line, @"Tests\.Company\..*")
            || Regex.IsMatch(line, @"Company\.Product\..*(Integration|System)Tests\..*"))
            && (Regex.IsMatch(line, @"Tests\.")
            || Regex.IsMatch(line, @"HzTests\."));
}