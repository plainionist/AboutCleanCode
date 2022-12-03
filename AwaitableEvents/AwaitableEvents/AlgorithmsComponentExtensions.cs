using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitableEvents;

public static class AlgorithmsComponentExtensions
{
    public record AlgoResultHandle(IAlgorithmsComponent Component, Guid RequestId);

    public static AlgoResultHandle RunAsync(this IAlgorithmsComponent self, AlgorithmInput input)
    {
        self.RunAlgorithm(input);
        return new AlgoResultHandle(self, input.RequestId);
    }

    public static TaskAwaiter<AlgorithmResult> GetAwaiter(this AlgoResultHandle handle)
    {
        var tcs = new TaskCompletionSource<AlgorithmResult>();

        void OnAlgorithmFinished(object _, AlgorithmFinishedEventArgs e)
        {
            if ( e.Result.RequestId != handle.RequestId)
            {
                return;
            }

            handle.Component.AlgorithmFinished -= OnAlgorithmFinished;
            tcs.SetResult(e.Result);
        }

        handle.Component.AlgorithmFinished += OnAlgorithmFinished;

        return tcs.Task.GetAwaiter();
    }
}
