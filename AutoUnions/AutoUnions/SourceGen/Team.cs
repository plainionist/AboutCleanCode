using Dunet;

namespace AutoUnions.SourceGen;

[Union]
public partial record Team
{
    public partial record DevelopmentTeam(Capacity Capacity, float Velocity);
    public partial record NonDevelopmentTeam();
    public partial record SupplierTeam(float TotalCapacity);

    public required string Name { get; init; }
    public required IList<string> AreaPaths { get; init; }
}

