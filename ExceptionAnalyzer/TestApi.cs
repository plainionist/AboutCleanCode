namespace ExceptionAnalyzer;

public class TestApi
{
    public bool IsSameExceptionIgnoringTestClass(string exceptionText1, string exceptionText2)
    {
        var parser = new ExceptionParser();
        var exception1 = parser.ExtractException(exceptionText1);
        var exception2 = parser.ExtractException(exceptionText2);
        return new TestClassInvariantComparer().IsSame(exception1, exception2);
    }
}
