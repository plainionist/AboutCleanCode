using System;
using System.Threading;

namespace AwaitableEvents;

public record AlgorithmPromise(IAlgorithmsComponent Component, Guid RequestId)
{
    private readonly ManualResetEvent myEvent = new(false);
    private AlgorithmResult myResult;
    private Action<AlgorithmPromise> myContinuation;

    public AlgorithmResult Result
    {
        get
        {
            myEvent.WaitOne();
            return myResult;
        }
    }

    public bool HasResult { get; private set; }

    public void SetResult(AlgorithmResult result)
    {
        HasResult = true;
        myResult = result;

        myEvent.Set();

        myContinuation?.Invoke(this);
    }

    public void ContinueWith(Action<AlgorithmPromise> continuation) =>
        myContinuation = continuation;
}
