using System;

namespace Promise.Step4;

public interface IComponent
{
    Promise Execute(Request request);
}

public class Request { }
public class Response { }

public class Promise
{
    private Action<Response> myContinuation;

    public void ContinueWith(Action<Response> continuation)
    {
        myContinuation = continuation;
    }

    internal void SetResult(Response result)
    {
        myContinuation(result);
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
}

public class Client
{
    public void Run(IComponent component)
    {
        component.Execute(new Request())
            .ContinueWith(response => Console.WriteLine("Response: " + response));
    }
}
