using System;
using System.Collections.Generic;

namespace ExceptionAnalyzer;

class ExceptionInfo
{
    public string Type { get; init; }
    public string Message { get; init; }
    public IReadOnlyCollection<StackTraceLine> StackTrace { get; init; }
}

public abstract record StackTraceLine(string Value, string NameSpace, string ClassName, string Method, string Parameters)
{
    public T Select<T>(Func<TestClassStackTraceLine, T> case1, Func<TestAbstractionStackTraceLine, T> case2, Func<ProductStackTraceLine, T> case3) =>
        this switch
        {
            TestClassStackTraceLine l => case1(l),
            TestAbstractionStackTraceLine l => case2(l),
            ProductStackTraceLine l => case3(l)
        };
}

public record TestClassStackTraceLine(string Value, string NameSpace, string ClassName, string Method, string Parameters)
    : StackTraceLine(Value, NameSpace, ClassName, Method, Parameters);

public record TestAbstractionStackTraceLine(string Value, string NameSpace, string ClassName, string Method, string Parameters)
    : StackTraceLine(Value, NameSpace, ClassName, Method, Parameters);

public record ProductStackTraceLine(string Value, string NameSpace, string ClassName, string Method, string Parameters)
    : StackTraceLine(Value, NameSpace, ClassName, Method, Parameters);

static class StackTraceLineFactory
{
    public static StackTraceLine Create(string value, string nameSpace, string className, string method, string parameters)
    {
        if ((nameSpace.StartsWith("Tests.Company.") || nameSpace.EndsWith(".IntegrationTests") || nameSpace.EndsWith(".SystemTests"))
            && (className.EndsWith("Tests") || className.EndsWith("HzTests")))
        {
            return new TestClassStackTraceLine(value, nameSpace, className, method, parameters);
        }
        else if ((nameSpace.StartsWith("Tests.Company.") || nameSpace.EndsWith(".IntegrationTests") || nameSpace.EndsWith(".SystemTests"))
            && !(className.EndsWith("Tests") || className.EndsWith("HzTests")))
        {
            return new TestAbstractionStackTraceLine(value, nameSpace, className, method, parameters);
        }
        else
        {
            return new ProductStackTraceLine(value, nameSpace, className, method, parameters);
        }
    }
}
