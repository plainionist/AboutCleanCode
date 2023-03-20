using System.Runtime.CompilerServices;

namespace AboutCleanCode;

class RecordValidator
{
    public static RecordValidator Verify => new();

    public RecordValidator NotNull<T>(T argument, [CallerArgumentExpression("argument")] string argumentName = null) where T : class
    {
        Contract.RequiresNotNull(argument, argumentName);
        return this;
    }

    public RecordValidator NotNullNotEmpty(string argument, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        Contract.RequiresNotNullNotEmpty(argument, argumentName);
        return this;
    }

    public bool IsValid() => true;
}