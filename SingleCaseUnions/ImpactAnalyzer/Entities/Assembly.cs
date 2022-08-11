
namespace ImpactAnalyzer.Entities
{
    public record Assembly
    {
        private readonly string myValue;

        public Assembly(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            myValue = value;
        }

        public static implicit operator string(Assembly v) => v.myValue;

        public override string ToString() => myValue;
    }
}
