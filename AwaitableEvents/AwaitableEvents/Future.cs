using System;

namespace AwaitableEvents;

public class Future
{
    public Future(IAlgorithmsComponent algorithmsComponent, Guid requestId)
    {
        AlgorithmsComponent = algorithmsComponent;
        RequestId = requestId;
    }

    public IAlgorithmsComponent AlgorithmsComponent { get; }
    public Guid RequestId { get; }
}