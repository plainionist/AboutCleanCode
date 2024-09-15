namespace AutoCtor;

public class PersonRepository
{
    private readonly ILogger myLogger;
    private readonly ISqlConnectionFactory myConnectionFactory;
    private readonly Configuration myConfiguration;

    public PersonRepository(
        ILogger logger,
        ISqlConnectionFactory connectionFactory,
        Configuration configuration)
    {
        myLogger = logger;
        myConnectionFactory = connectionFactory;
        myConfiguration = configuration;
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

// [AutoConstructor]
// public partial class PersonRepository
// {
//     private readonly ILogger myLogger;
//     private readonly string myConnectionString;

//     [AutoConstructorInitializer]
//     private void  Initializer()
//     {
//         myLogger.Information($"Creating Person table on demand at '{myConnectionString}' ...");

//         // TODO: implement

//         myLogger.Information("Person table created!");
//     }
// }




