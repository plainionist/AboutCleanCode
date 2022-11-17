using System;

namespace AwaitableEvents;

public interface IAlgorithmsComponent
{
    void RunAsync(InputData input);

    event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;
}
