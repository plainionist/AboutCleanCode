namespace ImpactAnalyzer.Entities
{
    public class NameSpace
    {
        public NameSpace(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            Value = value;
        }

        public string Value { get; }
    }
}