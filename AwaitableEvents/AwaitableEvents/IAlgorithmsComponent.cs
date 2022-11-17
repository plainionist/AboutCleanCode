using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitableEvents;

public interface IAlgorithmsComponent
{
    Future RunAsync(InputData input);

    event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;
}

public static class AlgorithmsComponentExtensions
{
    public static TaskAwaiter<AlgorithmResult> GetAwaiter(this Future self)
    {
        var tcs = new TaskCompletionSource<AlgorithmResult>();

        // TODO: we should probably check whether result is already available

        void OnAlgorithmFinished(object _, AlgorithmFinishedEventArgs e)
        {
            if (self.RequestId != e.RequestId)
            {
                return;
            }

            self.AlgorithmsComponent.AlgorithmFinished -= OnAlgorithmFinished;
            tcs.SetResult(e.Result);
        }

        self.AlgorithmsComponent.AlgorithmFinished += OnAlgorithmFinished;

        return tcs.Task.GetAwaiter();
    }
}

