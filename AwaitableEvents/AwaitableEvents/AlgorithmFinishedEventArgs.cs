using System;

namespace AwaitableEvents;

public class AlgorithmFinishedEventArgs : EventArgs
{
    public AlgorithmFinishedEventArgs(AlgorithmResult result)
    {
        Result = result;
    }

    public AlgorithmResult Result { get; }
}