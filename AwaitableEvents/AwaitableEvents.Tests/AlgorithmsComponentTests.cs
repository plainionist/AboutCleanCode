using System.Threading.Tasks;
using NUnit.Framework;

namespace AwaitableEvents.Tests;

public class Tests
{
    [Test]
    public async Task WaitForEvents()
    {
        var component = new AlgorithmsComponent();

        component.RunAsync(new InputData(21));

        var result = await component;

        Assert.That(result.Value, Is.EqualTo(42));
    }
}