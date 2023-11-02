using System;
using System.Threading.Tasks;

namespace Promise.Step7;

public interface IComponent
{
    Task<Response> Execute(Request request);
}

public class Request { }
public class Response { }

public class Component : IComponent
{
    private TaskCompletionSource<Response> myActivePromise;

    public Task<Response> Execute(Request request)
    {
        // TODO: do the work

        myActivePromise = new TaskCompletionSource<Response>();
        return myActivePromise.Task;
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
    public async void Run(IComponent component)
    {
        var response = await component.Execute(new Request());

        Console.WriteLine("Response: " + response);
    }
}

