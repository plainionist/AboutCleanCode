namespace Promise.Step1;

public interface IComponent
{
    Response Execute(Request request);
}

public class Request { }
public class Response { }
