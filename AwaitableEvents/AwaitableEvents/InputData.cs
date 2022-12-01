using System;

namespace AwaitableEvents;

public class AlgorithmInput
{
    public AlgorithmInput(int value)
    {
        RequestId = Guid.NewGuid();
        Value = value;
    }

    public Guid RequestId { get; }
    public int Value { get; }
}