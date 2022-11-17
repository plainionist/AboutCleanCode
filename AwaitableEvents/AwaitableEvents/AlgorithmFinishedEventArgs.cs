using System;

namespace AwaitableEvents;

public class AlgorithmFinishedEventArgs : EventArgs
{
    public AlgorithmFinishedEventArgs(Guid requestId, AlgorithmResult result)
    {
        RequestId = requestId;
        Result = result;
    }

    public Guid RequestId { get; }
    public AlgorithmResult Result { get; }
}