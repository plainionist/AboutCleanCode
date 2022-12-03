using System;

namespace AwaitableEvents;

public class AlgorithmResult
{
    public AlgorithmResult(Guid requestId, int value)
    {
        RequestId = requestId;
        Value = value;
    }

    public Guid RequestId { get; }
    public int Value { get; }
}