using System.Threading;
using NUnit.Framework;

namespace AwaitableEvents.Tests;

public class AlgorithmsComponentTests
{
    [Test]
    public void WaitForEvents()
    {
        var component = new AlgorithmsComponent();

        component.RunAlgorithm(new AlgorithmInput(21));

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