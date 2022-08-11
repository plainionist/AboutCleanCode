namespace ImpactAnalyzer.Entities
{
    public class CodeType
    {
        public CodeType(string assembly, string nameSpace, string name)
        {
            Contract.RequiresNotNullNotEmpty(assembly, nameof(assembly));
            Contract.RequiresNotNullNotEmpty(nameSpace, nameof(nameSpace));
            Contract.RequiresNotNullNotEmpty(name, nameof(name));

            Assembly = assembly;
            NameSpace = nameSpace;
            Name = name;
        }

        public string Assembly { get; }
        public string NameSpace { get; }
        public string Name { get; }
    }
}
