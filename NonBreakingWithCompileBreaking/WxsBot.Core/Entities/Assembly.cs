using System;

namespace WxsBot.Entities
{
    public enum ProjectOutputType
    {
        Library,
        Executable
    }

    public class Assembly : IEquatable<Assembly>
    {
        public Assembly(string name, ProjectOutputType outputType)
        {
            Contract.RequiresNotNullNotEmpty(name, nameof(name));

            Name = name;
            OutputType = outputType;
        }

        public string Name { get; }
        public ProjectOutputType OutputType { get; }
        public string FullName => Name + OutputType;

        public override int GetHashCode() => Name.ToLower().GetHashCode();

        public override bool Equals(object obj)
        {
            var other = obj as Assembly;
            if (other == null)
            {
                return false;
            }

            return other.Equals(this);
        }

        public bool Equals(Assembly other) =>
            Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
    }
}
