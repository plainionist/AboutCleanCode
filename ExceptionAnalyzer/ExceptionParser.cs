using System;
using System.Collections.Generic;
using System.Linq;

namespace ExceptionAnalyzer;

class ExceptionParser
{
    public ExceptionInfo ExtractException(string exceptionText)
    {
        var lines = exceptionText.Split(Environment.NewLine)
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        var exceptionType = lines[0].Split(':').First();
        var exceptionMessage = lines[0].Substring(lines[0].IndexOf(':') + 1);
        var stackTrace = GetStackTrance(lines);

        return new()
        {
            Type = exceptionType,
            Message = exceptionMessage,
            StackTrace = stackTrace
        };
    }

    private List<string> GetStackTrance(IEnumerable<string> lines)
    {
        return lines
            .Skip(1)
            .Select(x => x.Trim())
            .Where(x => x.StartsWith("at "))
            .Select(x => x.Substring(3))
            .Select(x => x.Substring(0, x.IndexOf('(')))
            .ToList();
    }
}