using System;

namespace Promise.Step5;

public interface IComponent
{
    Promise Execute(Request request);
}

public class Request { }
public class Response { }

public class Promise
{
    private Action<Promise> myContinuation;
    private Response myResult;
    private Exception myException;

    public Response Result
    {
        get { return HasResult ? myResult : throw new InvalidOperationException("Result not yet available"); }
    }

    public bool HasResult => myResult != null;

    public Exception Exception
    {
        get { return IsFaulted ? myException : null; }
    }

    public bool IsFaulted => myException != null;

    public void ContinueWith(Action<Promise> continuation)
    {
        myContinuation = continuation;
    }

    internal void SetResult(Response result)
    {
        myResult = result;
        myContinuation(this);
    }

    internal void SetException(Exception exception)
    {
        myException = exception;
        myContinuation(this);
    }
}

public class Component : IComponent
{
    private Promise myActivePromise;

    public Promise Execute(Request request)
    {
        // TODO: do the work

        myActivePromise = new Promise();
        return myActivePromise;
    }

    private void OnExecutionFinished()
    {
        myActivePromise.SetResult(new Response());
    }

    private void OnExecutionFailed(Exception ex)
    {
        myActivePromise.SetException(ex);
    }
}

public class Client
{
    public void Run(IComponent component)
    {
        component.Execute(new Request())
            .ContinueWith(x => Console.WriteLine("Response: " + (x.IsFaulted ? x.Exception : x.Result)));
    }
}
