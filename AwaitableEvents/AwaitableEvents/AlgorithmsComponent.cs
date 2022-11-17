using System;
using System.Threading.Tasks;

namespace AwaitableEvents;

public class AlgorithmsComponent : IAlgorithmsComponent
{
    public event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;

    public void RunAsync(InputData input)
    {
        // dummy implmentation
        Task.Delay(5000)
            .ContinueWith(_ => AlgorithmFinished?.Invoke(this, new AlgorithmFinishedEventArgs(new AlgorithmResult(42))));
    }
}
