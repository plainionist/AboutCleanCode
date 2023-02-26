using NUnit.Framework;

namespace ExceptionAnalyzer.Tests;

[TestFixture]
internal class TestStackTraceRemovalTests
{
    [Test]
    public void IdenticalStackTraces_True()
    {
        const string ex1 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.NoTestStackTrace()";
        const string ex2 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.NoTestStackTrace()";

        Assert.That(new TestApi().IsSameExceptionIgnoringTestClass(ex1, ex2), Is.True);
    }

    [Test]
    public void DifferentTestCasesSameRemainingStack_True()
    {
        const string ex1 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase1()";
        const string ex2 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase2()";

        Assert.That(new TestApi().IsSameExceptionIgnoringTestClass(ex1, ex2), Is.True);
    }


    [Test]
    public void MultipleDifferentTestCaseLinesSameRemainingStack_True()
    {
        const string ex1 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.Helper1()
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase1()";
        const string ex2 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.Helper2()
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase2()";

        Assert.That(new TestApi().IsSameExceptionIgnoringTestClass(ex1, ex2), Is.True);
    }

    [Test]
    public void SameTestCasesDifferentRemainingStack_False()
    {
        const string ex1 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.ExceptionPreProcessor()
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase1()";
        const string ex2 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase1()";

        Assert.That(new TestApi().IsSameExceptionIgnoringTestClass(ex1, ex2), Is.False);
    }

    [Test]
    public void DifferentTestCasesDifferentRemainingStack_False()
    {
        const string ex1 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.ExceptionPreProcessor()
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase1()";
        const string ex2 = @"
            System.NullReferenceException : Object reference not set to an instance of an object.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.TestCase2()";

        Assert.That(new TestApi().IsSameExceptionIgnoringTestClass(ex1, ex2), Is.False);
    }

    [Test]
    public void DifferentExceptionType_False()
    {
        const string ex1 = @"
            System.ArgumentException : Argument 'errorMessage' was null.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.NoTestStackTrace()";
        const string ex2 = @"
            System.ArgumentNullException : Argument 'errorMessage' was null.
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.NoTestStackTrace()";

        Assert.That(new TestApi().IsSameExceptionIgnoringTestClass(ex1, ex2), Is.False);
    }

    [Test]
    public void DifferentExceptionMessage_False()
    {
        const string ex1 = @"
            System.ApplicationException : That was unexpected
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.NoTestStackTrace()";
        const string ex2 = @"
            System.ApplicationException : This should not happen
            Stack Trace:
            at Company.Product.ExceptionPreProcessor.GetExceptionType(String errorMessage)
            at Company.Product.ExceptionPreProcessor.ExtractException(String errorMessage)
            at Company.Product.TestStackTraceRemoval.IsSame(String data1, String data2)
            at Company.Product.IntegrationTests.TestStackTraceRemovalTests.NoTestStackTrace()";

        Assert.That(new TestApi().IsSameExceptionIgnoringTestClass(ex1, ex2), Is.False);
    }
}
