using System;

namespace AwaitableEvents;

public interface IAlgorithmsComponent
{
    void RunAsync(AlgorithmInput input);

    event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;
}
