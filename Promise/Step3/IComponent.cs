using System;

namespace Promise.Step3;

public interface IComponent
{
    // This is a continuation: https://en.wikipedia.org/wiki/Continuation
    void Execute(Request request, Action<Response> onFinish);
}

public class Request { }
public class Response { }

public class Component : IComponent
{
    public void Execute(Request request, Action<Response> onFinish)
    {
        // TODO: do the work

        onFinish(new Response());
    }
}

public class Client
{
    public void Run(IComponent component)
    {
        component.Execute(new Request(), response => Console.WriteLine("Response: " + response));
    }
}
