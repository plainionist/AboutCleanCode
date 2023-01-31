namespace ExceptionAnalyzer;

public class TestApi
{
    public bool IsSameExceptionIgnoringTestClass(string exceptionText1, string exceptionText2)
    {
        return new TestClassInvariantExceptionComparer().IsSame(exceptionText1, exceptionText2);
    }
}
