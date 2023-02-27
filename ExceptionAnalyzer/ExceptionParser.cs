using System;
using System.Collections.Generic;
using System.Linq;

namespace ExceptionAnalyzer;

class ExceptionParser
{
    public ExceptionInfo ExtractException(string exceptionText)
    {
        var lines = ParseText(exceptionText);

        var (exceptionType, exceptionMessage) = ParseTypeAndMessage(lines);
        var stackTrace = GetStackTrance(lines);

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

    private static List<string> GetStackTrance(IEnumerable<string> lines)
    {
        return lines
            .Skip(1)
            .Select(x => x.Trim())
            .Where(x => x.StartsWith("at "))
            .Select(x => x[3..])
            .Select(x => x[..x.IndexOf('(')])
            .ToList();
    }
}