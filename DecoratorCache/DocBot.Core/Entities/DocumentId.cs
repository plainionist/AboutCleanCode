namespace DocBot.Entities
{
    public sealed record DocumentId
    {
        /// <summary />
        /// <param name="number">Wildcard '*' supported to specify a document without specfiic version</param>
        public DocumentId(string number, string type, string version)
        {
            Contract.RequiresNotNullNotEmpty(number, nameof(number));
            Contract.RequiresNotNullNotEmpty(type, nameof(type));
            Contract.RequiresNotNullNotEmpty(version, nameof(version));

            Number = number;
            Type = type;
            Version = version;
        }

        public string Number { get; }
        public string Type { get; }
        public string Version { get; }

        public bool IsExplictVersion => Version != "*";
    }
}
