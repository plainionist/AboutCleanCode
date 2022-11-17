using System;
using System.Threading.Tasks;

namespace AwaitableEvents;

public class AlgorithmsComponent : IAlgorithmsComponent
{
    public Guid LatestRequestId { get; private set; }

    public event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;

    public void RunAsync(InputData input)
    {
        LatestRequestId = input.RequestId;

        // dummy implmentation
        Task.Delay(5000)
            .ContinueWith(_ => AlgorithmFinished?.Invoke(this, new AlgorithmFinishedEventArgs(input.RequestId, new AlgorithmResult(42))));
    }
}
