using System;
using System.Threading;

namespace AwaitableEvents;

public class AlgorithmPromise
{
    private readonly ManualResetEvent myEvent = new(false);
    private readonly IAlgorithmsComponent myComponent;
    private readonly Guid myRequestId;
    private AlgorithmResult myResult;
    private Action<AlgorithmPromise> myContinuation;

    public AlgorithmPromise(IAlgorithmsComponent component, Guid requestId)
    {
        myComponent = component;
        myRequestId = requestId;

        myComponent.AlgorithmFinished += OnAlgorithmFinished;
    }

    private void OnAlgorithmFinished(object sender, AlgorithmFinishedEventArgs e)
    {
        if (myRequestId != e.Result.RequestId)
        {
            return;
        }

        myComponent.AlgorithmFinished -= OnAlgorithmFinished;

        HasResult = true;
        myResult = e.Result;
        myEvent.Set();

        myContinuation?.Invoke(this);
    }

    public AlgorithmResult Result
    {
        get
        {
            myEvent.WaitOne();
            return myResult;
        }
    }

    public bool HasResult { get; private set; }

    public void ContinueWith(Action<AlgorithmPromise> continuation) =>
        myContinuation = continuation;
}
