using System.Linq;

namespace ExceptionAnalyzer;

class TestClassInvariantComparer
{
    public bool IsSame(ExceptionInfo exception1, ExceptionInfo exception2)
    {
        if (exception1.Type != exception2.Type || exception1.Message != exception2.Message)
        {
            return false;
        }

        var filteredStackTrace1 = exception1.StackTrace
            .Where(x => x.GetApiType() != ApiType.TestClass)
            .ToList();

        var filteredStackTrace2 = exception2.StackTrace
            .Where(x => x.GetApiType() != ApiType.TestClass)
            .ToList();

        return !filteredStackTrace1.Except(filteredStackTrace2).Any();
    }
}
