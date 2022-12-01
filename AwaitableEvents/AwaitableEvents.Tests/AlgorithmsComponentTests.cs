using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AwaitableEvents.Tests;

public class Tests
{
    [Test]
    public void WaitForEvents()
    {
        var component = new AlgorithmsComponent();

        component.RunAsync(new AlgorithmInput(21));

        // TODO: isn't there a better way?

        var evt = new ManualResetEvent(false);
        AlgorithmResult result =null;
        component.AlgorithmFinished += (_, e) =>
        {
            result = e.Result;
            evt.Set();
        };

        evt.WaitOne();

        Assert.That(result.Value, Is.EqualTo(42));
    }
}