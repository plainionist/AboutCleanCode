using System.Collections.Generic;

namespace ExceptionAnalyzer;

class ExceptionInfo
{
    public string Type { get; init; }
    public string Message { get; init; }
    public IReadOnlyCollection<StackTraceLine> StackTrace { get; init; }
}

public record StackTraceLine(string Value, bool IsTestClass, string NameSpace, string ClassName, string Method, string Parameters);
