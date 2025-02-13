namespace HowToInCA.Application.FeatureA;

public record NuGetPackageReference
{
    public NuGetPackageReference(string name, Version version)
    {
        Contract.RequiresNotNullNotEmpty(name);
        Contract.RequiresNotNull(name);

        Name = name;
        Version = version;
    }

    public string Name { get; }
    public Version Version { get; }
}
