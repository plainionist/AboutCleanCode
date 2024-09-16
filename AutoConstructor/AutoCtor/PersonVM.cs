namespace AutoCtor;

[AutoConstructor]
public partial class PersonVM
{
    public string Name { get; }
    public string Status { get; }

    [AutoConstructorInitializer]
    private void Initializer()
    {
        Contract.RequiresNotNullNotEmpty(nameof(Name));
        Contract.Requires(Name.Length <= 100, "Name must not be longer than 100 characters");
    }
}

