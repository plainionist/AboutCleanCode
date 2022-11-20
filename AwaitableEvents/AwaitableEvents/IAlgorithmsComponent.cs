using System;
using System.Runtime.CompilerServices;

namespace AwaitableEvents;

public interface IAlgorithmsComponent
{
    Future RunAsync(InputData input);

    event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;
}

public static class AlgorithmsComponentExtensions
{
    public static IAwaiter<AlgorithmResult> GetAwaiter(this Future self) =>
        // TODO: we should probably check whether result is already available
        new AlgorithmsAwaiter(self);

    private class AlgorithmsAwaiter : IAwaiter<AlgorithmResult>
    {
        private readonly IAlgorithmsComponent myComponent;
        private readonly Guid myRequestId;
        private AlgorithmResult myResult;
        private Action myContinuation;

        public AlgorithmsAwaiter(Future future)
        {
            myComponent = future.AlgorithmsComponent;
            myComponent.AlgorithmFinished += OnAlgorithmFinished;

            myRequestId = future.RequestId;
        }

        private void OnAlgorithmFinished(object sender, AlgorithmFinishedEventArgs e)
        {
            if (myRequestId != e.RequestId)
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

public interface IAwaiter<out TResult> : INotifyCompletion
{
    bool IsCompleted { get; }
    TResult GetResult();
}


