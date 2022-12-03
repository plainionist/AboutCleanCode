using System;
using System.Runtime.CompilerServices;

namespace AwaitableEvents;

public static class AlgorithmsComponentExtensions
{
    public record AlgorithmFuture(IAlgorithmsComponent Component, Guid RequestId);

    public static AlgorithmFuture RunAsync(this IAlgorithmsComponent self, AlgorithmInput input)
    {
        self.RunAlgorithm(input);
        return new AlgorithmFuture(self, input.RequestId);
    }

    public static AlgorithmsAwaiter GetAwaiter(this AlgorithmFuture future) =>
        new AlgorithmsAwaiter(future);

    public class AlgorithmsAwaiter : INotifyCompletion
    {
        private readonly IAlgorithmsComponent myComponent;
        private readonly Guid myRequestId;
        private AlgorithmResult myResult;
        private Action myContinuation;

        public AlgorithmsAwaiter(AlgorithmFuture future)
        {
            myComponent = future.Component;
            myComponent.AlgorithmFinished += OnAlgorithmFinished;

            myRequestId = future.RequestId;
        }

        private void OnAlgorithmFinished(object sender, AlgorithmFinishedEventArgs e)
        {
            if (myRequestId != e.Result.RequestId)
            {
                return;
            }

            myComponent.AlgorithmFinished -= OnAlgorithmFinished;
            myResult = e.Result;

            myContinuation();
        }

        public bool IsCompleted => myResult != null;

        public AlgorithmResult GetResult() => myResult;

        public void OnCompleted(Action continuation) =>
            myContinuation = continuation;
    }
}
