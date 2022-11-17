using System.Threading.Tasks;
using NUnit.Framework;

namespace AwaitableEvents.Tests;

public class Tests
{
    [Test]
    public async Task WaitForEvents()
    {
        var component = new AlgorithmsComponent();

        var result = await component.RunAsync(new InputData(21));

        Assert.That(result.Value, Is.EqualTo(42));
    }
}