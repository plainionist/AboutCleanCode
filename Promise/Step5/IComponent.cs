using System;
using System.Runtime.CompilerServices;

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

    public Response Result
    {
        get
        {
            return HasResult ? myResult : throw new InvalidOperationException("Result not yet available");
        }
    }

    public bool HasResult => myResult != null;

    public void ContinueWith(Action<Promise> continuation)
    {
        myContinuation = continuation;
    }

    internal void SetResult(Response result)
    {
        myResult = result;
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
}

public class Client
{
    public async void Run(IComponent component)
    {
        var response = await component.Execute(new Request());

        Console.WriteLine("Response: " + response);
    }
}

public static class PromiseExtensions
{
    public static IAwaiter GetAwaiter(this Promise promise) =>
        new Awaiter(promise);

    // TODO: show screenshots of compiler error messages if APIs are missing
    public interface IAwaiter : INotifyCompletion
    {
        bool IsCompleted { get; }

        public Response GetResult();
    }

    private class Awaiter : IAwaiter
    {
        private readonly Promise myPromise;

        public Awaiter(Promise promise)
        {
            myPromise = promise;
        }

        public bool IsCompleted => myPromise.HasResult;

        public Response GetResult() => myPromise.Result;

        public void OnCompleted(Action continuation) =>
            myPromise.ContinueWith(_ => continuation());
    }
}
