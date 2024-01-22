using System;

namespace AsyncAwait;

public class Promise
{
    private Action<Promise> myContinuation;
    private Response myResult;
    private Exception myException;

    public Response Result => HasResult ? myResult : throw new InvalidOperationException("Result not yet available");
    public bool HasResult => myResult != null;

    public Exception Exception => IsFaulted ? myException : null;
    public bool IsFaulted => myException != null;

    public void ContinueWith(Action<Promise> continuation) => myContinuation = continuation;

    internal void SetResult(Response response)
    {
        myResult = response;
        myContinuation(this);
    }

    internal void SetException(Exception exception)
    {
        myException = exception;
        myContinuation(this);
    }
}