using System;

namespace Promise.Step2;

public interface IComponent
{
    void Execute(Request request);

    event EventHandler<FinishedEventArgs> Finished;
}

public class Request { }
public class Response { }

public class FinishedEventArgs : EventArgs
{
    public FinishedEventArgs(Response response)
    {
        Response = Response;
    }

    public Response Response { get; }
}

public class Component : IComponent
{
    public event EventHandler<FinishedEventArgs> Finished;

    public void Execute(Request request)
    {
        // TODO: do the work
 
        Finished?.Invoke(this, new FinishedEventArgs(new Response()));
    }
}

public class Client
{
    public void Run(IComponent component)
    {
        component.Finished += OnFinished;
        component.Execute(new Request());
    }

    private void OnFinished(object sender, FinishedEventArgs e)
    {
        Console.WriteLine("Response: " + e.Response);
    }
}