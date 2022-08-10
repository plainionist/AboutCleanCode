
namespace ImpactAnalyzer.Entities
{
    public record Assembly_Record
    {
        private readonly string myValue;

        public Assembly_Record(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            myValue = value;
        }

        public static implicit operator string(Assembly_Record v) => v.myValue;

        public override string ToString() => myValue;
    }

    public struct Assembly_Strucut
    {
        private readonly string myValue;

        public Assembly_Strucut(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            myValue = value;
        }

        public static implicit operator string(Assembly_Strucut v) => v.myValue;

        public override string ToString() => myValue;
    }
}
