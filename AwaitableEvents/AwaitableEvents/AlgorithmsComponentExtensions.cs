using System;
using System.Runtime.CompilerServices;

namespace AwaitableEvents;

public static class AlgorithmsComponentExtensions
{
    public static AlgorithmPromise RunAsync(this IAlgorithmsComponent self, AlgorithmInput input)
    {
        self.RunAlgorithm(input);
        return new AlgorithmPromise(self, input.RequestId);
    }

    public static AlgorithmsAwaiter GetAwaiter(this AlgorithmPromise promise) =>
        new AlgorithmsAwaiter(promise);

    public class AlgorithmsAwaiter : INotifyCompletion
    {
        private readonly AlgorithmPromise myPromise;

        public AlgorithmsAwaiter(AlgorithmPromise promise)
        {
            myPromise = promise;
        }

        public bool IsCompleted => myPromise.HasResult;

        public AlgorithmResult GetResult() => myPromise.Result;

        public void OnCompleted(Action continuation) =>
            myPromise.ContinueWith(_ => continuation());
    }
}
