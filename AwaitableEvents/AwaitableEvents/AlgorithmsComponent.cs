using System;
using System.Threading.Tasks;

namespace AwaitableEvents;

public class AlgorithmsComponent : IAlgorithmsComponent
{
    public event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;

    public Future RunAsync(InputData input)
    {
        // dummy implmentation
        Task.Delay(5000)
            .ContinueWith(_ => AlgorithmFinished?.Invoke(this, new AlgorithmFinishedEventArgs(input.RequestId, new AlgorithmResult(42))));

        return new Future(this, input.RequestId);
    }
}
