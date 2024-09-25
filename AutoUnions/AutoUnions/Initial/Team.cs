namespace AutoUnions.Initial;

public class Team(string name)
{
    public string Name { get; } = name;
    public IList<string> AreaPath { get; } = [];

    public bool IsSupplier { get; set; }
    public bool IsDevelopment { get; set; }

    public Capacity? Capacity { get; set; }
    public float Velocity { get; set; }
}



