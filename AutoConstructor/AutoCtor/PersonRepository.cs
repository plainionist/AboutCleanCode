namespace AutoCtor;

[AutoConstructor]
public partial class PersonRepository
{
    private readonly ILogger myLogger;
    private readonly ISqlConnectionFactory myConnectionFactory;
    private readonly Configuration myConfiguration;

    [AutoConstructorInitializer]
    private void Initialize()
    {
        if (!TableExists())
        {
            myLogger.Information("Initializing Person table");

            CreateTable();
        }
    }

    private void CreateTable()
    {
        // TODO: implement
    }

    private bool TableExists()
    {
        return false;
    }


    public IReadOnlyCollection<Person> Read()
    {
        using var connection = myConnectionFactory.Create();

        myLogger.Information("Reading ...");

        // TODO: implement
        var config = myConfiguration.GetSection("Persons");

        return null;
    }

}

