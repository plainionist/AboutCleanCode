using System.Collections.Generic;

namespace ExceptionAnalyzer;

class ExceptionInfo
{
    public string Type { get; init; }
    public string Message { get; init; }
    public IReadOnlyCollection<StackTraceLine> StackTrace { get; init; }
}

public record StackTraceLine(string Value, string NameSpace, string ClassName, string Method, string Parameters)
{
    public ApiType GetApiType()
    {
        if ((NameSpace.StartsWith("Tests.Company.") || NameSpace.EndsWith(".IntegrationTests") || NameSpace.EndsWith(".SystemTests"))
            && (ClassName.EndsWith("Tests") || ClassName.EndsWith("HzTests")))
        {
            return ApiType.TestClass;
        }
        else if ((NameSpace.StartsWith("Tests.Company.") || NameSpace.EndsWith(".IntegrationTests") || NameSpace.EndsWith(".SystemTests"))
            && !(ClassName.EndsWith("Tests") || ClassName.EndsWith("HzTests")))
        {
            return ApiType.TestAbstraction;
        }
        else
        {
            return ApiType.ProductCode;
        }
    }
}

public enum ApiType
{
    TestClass,
    TestAbstraction,
    ProductCode
}
