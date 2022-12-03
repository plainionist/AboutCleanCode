using System;
using System.Runtime.CompilerServices;

namespace AwaitableEvents;

public static partial class AlgorithmsComponentExtensions
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
        private readonly IAlgorithmsComponent myComponent;
        private readonly AlgorithmPromise myPromise;
        private Action myContinuation;

        public AlgorithmsAwaiter(AlgorithmPromise promise)
        {
            myComponent = promise.Component;
            myComponent.AlgorithmFinished += OnAlgorithmFinished;

            myPromise = promise;
        }

        private void OnAlgorithmFinished(object sender, AlgorithmFinishedEventArgs e)
        {
            if (myPromise.RequestId != e.Result.RequestId)
            {
                return;
            }

            myComponent.AlgorithmFinished -= OnAlgorithmFinished;

            myPromise.SetResult(e.Result);

            myContinuation();
        }

        public bool IsCompleted => myPromise.HasResult;

        public AlgorithmResult GetResult() => myPromise.Result;

        public void OnCompleted(Action continuation) =>
            myContinuation = continuation;
    }
}
