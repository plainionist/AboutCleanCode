using OneOf;

namespace AutoUnions.Generics;

public abstract class AbstractTeam(string name)
{
    public string Name { get; } = name;
    public IList<string> AreaPath { get; } = [];
}

public class DevelopmentTeam(string name) : AbstractTeam(name)
{
    public required Capacity Capacity { get; init; }
    public float Velocity { get; set; }
}

public class NonDevelopmentTeam(string name) : AbstractTeam(name) { }

public class SupplierTeam(string name) : AbstractTeam(name)
{
    public float TotalCapacity { get; set; }
}

[GenerateOneOf]
public partial class Team : OneOfBase<DevelopmentTeam, NonDevelopmentTeam, SupplierTeam> { }

