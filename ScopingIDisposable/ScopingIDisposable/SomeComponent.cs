namespace ScopingIDisposable;

internal class SomeComponent
{
    private ILogger myLogger;

    public SomeComponent(ILogger logger)
    {
        myLogger = logger;
    }

    public void HeavyComputation()
    {
        using var logger = myLogger.Scope(this);

        // ... all the logic goes here ...

    }
}
