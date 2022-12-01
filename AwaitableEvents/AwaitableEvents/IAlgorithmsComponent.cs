using System;

namespace AwaitableEvents;

public interface IAlgorithmsComponent
{
    void RunAlgorithm(AlgorithmInput input);

    event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;
}
