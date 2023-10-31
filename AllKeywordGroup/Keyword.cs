
public record Keyword
{
    public static readonly Keyword All = new("All");
    
    public Keyword(string value)
    {
        Contract.RequiresNotNullNotEmpty(value);

        Value = value;
    }

    public string Value { get; }
}
