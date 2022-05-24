using System;
using System.IO;

namespace WxsBot.Entities
{
    public class Bundle : IEquatable<Bundle>
    {
        public Bundle(string path)
        {
            Contract.RequiresNotNullNotEmpty(path, nameof(Path));

            Path = path;
            Name = System.IO.Path.GetFileName(path);
        }

        public string Name { get; }

        public string Path { get; }

        public override int GetHashCode() => Name.ToLower().GetHashCode();

        public override bool Equals(object obj)
        {
            var other = obj as Bundle;
            if (other == null)
            {
                return false;
            }

            return other.Equals(this);
        }

        public bool Equals(Bundle other)
        {
            return Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsValid() =>
            File.Exists(System.IO.Path.Combine(Path, Name + ".sln"));
    }
}
