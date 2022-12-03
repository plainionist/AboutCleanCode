using System;
using System.ComponentModel;
using System.Threading;

namespace AwaitableEvents;

public class AlgorithmsComponent : IAlgorithmsComponent
{
    private readonly BackgroundWorker myWorker;

    public AlgorithmsComponent()
    {
        myWorker = new BackgroundWorker();
        myWorker.DoWork += OnDoWork;
        myWorker.RunWorkerCompleted += OnCompleted;
    }

    private void OnDoWork(object sender, DoWorkEventArgs e)
    {
        var input = (AlgorithmInput)e.Argument;

        // dummy implementation

        Thread.Sleep(2000);

        e.Result = new AlgorithmResult(input.RequestId, input.Value * 2);
    }

    private void OnCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        var result = (AlgorithmResult)e.Result;
        AlgorithmFinished?.Invoke(this, new AlgorithmFinishedEventArgs(result));
    }

    public event EventHandler<AlgorithmFinishedEventArgs> AlgorithmFinished;

    public void RunAlgorithm(AlgorithmInput input)
    {
        myWorker.RunWorkerAsync(input);
    }
}
