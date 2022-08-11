
namespace ImpactAnalyzer.Entities
{
    public class Assembly
    {
        public Assembly(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj) =>
            obj is Assembly other ? Value == other.Value : false;

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Assembly lhs, Assembly rhs) =>
            ReferenceEquals(lhs, null) ? rhs == null : lhs.Equals(rhs);

        public static bool operator !=(Assembly lhs, Assembly rhs) => !(lhs == rhs);
    }
}
