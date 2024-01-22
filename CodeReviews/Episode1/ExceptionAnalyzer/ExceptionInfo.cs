using System.Collections.Generic;

namespace ExceptionAnalyzer;

public class ExceptionInfo
{
    public string Type { get; init; }
    public string Message { get; init; }
    public IReadOnlyCollection<string> StackTrace { get; init; }
}