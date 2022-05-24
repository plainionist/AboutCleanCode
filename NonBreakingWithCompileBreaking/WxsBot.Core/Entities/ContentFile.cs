using System;
using System.Collections.Generic;

namespace WxsBot.Entities
{
    public class ContentFile : IEquatable<ContentFile>
    {
        public ContentFile(VsProject project, string path)
        {
            Path = path;
            Project = project;
        }

        public string Path { get; }

        public VsProject Project { get; }

        public string Filename => System.IO.Path.GetFileName(Path);

        public string Directory => System.IO.Path.GetDirectoryName(Path);

        public IEnumerable<string> GetFolderNames() => Directory.Split('/', '\\');

        public override int GetHashCode() => Path.ToLower().GetHashCode();

        public override bool Equals(object obj)
        {
            var other = obj as ContentFile;
            if (other == null)
            {
                return false;
            }
            return other.Equals(this);
        }

        public bool Equals(ContentFile other)
        {
            return Path.Equals(other.Path, StringComparison.OrdinalIgnoreCase);
        }
    }
}
