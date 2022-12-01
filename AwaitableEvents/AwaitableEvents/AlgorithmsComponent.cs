using System;
using System.Threading.Tasks;

namespace AwaitableEvents;

public class AlgorithmsComponent : IAlgorithmsComponent
{
    public event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;

    public void RunAsync(AlgorithmInput input)
    {
        var requestId = input.RequestId;

        ComputeAlgorithmResult(input)
            .ContinueWith(t => RaiseAlgorithmFinished(requestId, t));
    }

    private Task<AlgorithmResult> ComputeAlgorithmResult(AlgorithmInput input) =>
        // dummy implmentation
        Task.Delay(5000)
            .ContinueWith(_ => new AlgorithmResult(input.Value * 2));

    private void RaiseAlgorithmFinished(Guid requestId, Task<AlgorithmResult> response) =>
        AlgorithmFinished?.Invoke(this, new AlgorithmFinishedEventArgs(requestId, response.Result));
}
