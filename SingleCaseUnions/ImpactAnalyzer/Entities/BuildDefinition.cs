    public record BuildDefinitionName
    {
        private readonly string myValue;

        public BuildDefinitionName(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            myValue = value;
        }

        public static implicit operator string(BuildDefinitionName v) => v.myValue;

        public override string ToString() => myValue;
    }

