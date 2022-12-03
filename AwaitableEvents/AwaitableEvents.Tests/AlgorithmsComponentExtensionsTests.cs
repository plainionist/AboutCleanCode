using System.Threading.Tasks;
using NUnit.Framework;

namespace AwaitableEvents.Tests;

public class AlgorithmsComponentExtensionsTests
{
    [Test]
    public async Task AwaitingResults()
    {
        var component = new AlgorithmsComponent();

        var result = await component.RunAsync(new AlgorithmInput(21));

        Assert.That(result.Value, Is.EqualTo(42));
    }
}