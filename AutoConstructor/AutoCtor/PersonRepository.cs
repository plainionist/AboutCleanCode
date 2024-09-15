namespace AutoCtor;

[AutoConstructor]
public partial class PersonRepository
{
    private readonly ILogger myLogger;
    private readonly string myConnectionString;

    [AutoConstructorInitializer]
    private void  Initializer()
    {
        myLogger.Information($"Creating Person table on demand at '{myConnectionString}' ...");

        // TODO: implement

        myLogger.Information("Person table created!");
    }
}
