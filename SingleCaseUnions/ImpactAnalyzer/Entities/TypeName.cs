namespace ImpactAnalyzer.Entities
{
    public class TypeName
    {
        public TypeName(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            Value = value;
        }

        public string Value { get; }
    }

}