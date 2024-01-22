using NUnit.Framework;

namespace ExceptionAnalyzer.Tests;

[TestFixture]
internal class StackTraceLineParserTests
{
    [Test]
    public void MethodWithParameters()
    {
        var testApi = new TestApi();

        var stackTraceLine = testApi.ParseStackTraceLine("Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException(string data1, string data2)");
        Assert.Multiple(() =>
        {
            Assert.That(stackTraceLine.NameSpace, Is.EqualTo("Company.Product.IntegrationTests.TestAbstractions"));
            Assert.That(stackTraceLine.ClassName, Is.EqualTo("ExceptionHelper"));
            Assert.That(stackTraceLine.Method, Is.EqualTo("PrepareException"));
            Assert.That(stackTraceLine.Parameters, Is.EqualTo("string data1, string data2"));
        });
    }

    [Test]
    public void MethodWithoutParameters()
    {
        var testApi = new TestApi();

        var stackTraceLine = testApi.ParseStackTraceLine("Company.Product.IntegrationTests.TestAbstractions.ExceptionHelper.PrepareException()");
        Assert.Multiple(() =>
        {
            Assert.That(stackTraceLine.NameSpace, Is.EqualTo("Company.Product.IntegrationTests.TestAbstractions"));
            Assert.That(stackTraceLine.ClassName, Is.EqualTo("ExceptionHelper"));
            Assert.That(stackTraceLine.Method, Is.EqualTo("PrepareException"));
            Assert.That(stackTraceLine.Parameters, Is.Empty);
        });
    }
}
