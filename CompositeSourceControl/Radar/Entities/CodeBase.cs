using System;

namespace Radar.Entities;

public sealed record CodeBase : IEquatable<CodeBase>
{
    private readonly int myHashCode;

    public CodeBase(string name)
    {
        Contract.RequiresNotNullNotEmpty(name, nameof(name));

        Value = name;

        // cache for performance reasons
        myHashCode = Value.ToLower().GetHashCode();
    }

    public string Value { get; }

    public bool Equals(CodeBase other) => other != null && other.Value.Equals(Value, StringComparison.OrdinalIgnoreCase);
    public override int GetHashCode() => myHashCode;
    public override string ToString() => Value;
}
