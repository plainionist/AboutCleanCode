
namespace AutoUnions;

public class Capacity
{
    public float Total { get; internal set; }

    public float Get((DateTime, DateTime) range)
    {
        throw new NotImplementedException();
    }
}