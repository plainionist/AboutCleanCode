using System;

namespace AwaitableEvents;

public class InputData
{
    public InputData(int value)
    {
        RequestId = Guid.NewGuid();
        Value = value;
    }

    public Guid RequestId { get; }
    public int Value { get; }
}