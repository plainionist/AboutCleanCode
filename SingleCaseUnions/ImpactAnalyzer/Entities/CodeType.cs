namespace ImpactAnalyzer.Entities
{
    public class CodeType
    {
        public CodeType(Assembly assembly, NameSpace nameSpace, TypeName name)
        {
            Contract.RequiresNotNull(assembly, nameof(assembly));
            Contract.RequiresNotNull(nameSpace, nameof(nameSpace));
            Contract.RequiresNotNull(name, nameof(name));

            Assembly = assembly;
            NameSpace = nameSpace;
            Name = name;
        }

        public Assembly Assembly { get; }
        public NameSpace NameSpace { get; }
        public TypeName Name { get; }
    }
}
