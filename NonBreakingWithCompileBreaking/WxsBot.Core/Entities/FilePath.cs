using System;
using System.Collections.Generic;
using System.Linq;

namespace WxsBot.Entities
{
    /// <summary>
    /// Represents a logical file path represented with unified directory separator.
    /// Can be TFS ServerItem, absolute file path from local file system or a relative path element.
    /// </summary>
    public sealed record FilePath : IEquatable<FilePath>
    {
        private readonly IReadOnlyList<string> myValue;

        public FilePath(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));
            myValue = value.Split('/', '\\');
        }

        public FilePath(IEnumerable<string> values)
        {
            Contract.RequiresNotNull(values, nameof(values));
            myValue = values.ToList();
        }

        public bool Equals(FilePath other) =>
            other != null && AsUnix() == other.AsUnix();

        public override int GetHashCode() => AsUnix().GetHashCode();

        public override string ToString() => AsUnix();

        public string AsUnix() => string.Join("/", myValue);

        public string AsWindows() => string.Join(@"\", myValue);

        public static implicit operator FilePath(string v) => new(v);

        public static FilePath operator /(FilePath a, string b) => new(a.myValue.Append(b));

        public bool StartsWith(FilePath other) =>
            myValue.Count >= other.myValue.Count
            && other.myValue
                .Select((x, i) => x.Equals(myValue[i], StringComparison.OrdinalIgnoreCase))
                .All(x => x);

        public bool ExistsInWorkspace(string workspaceRoot) =>
            !StartsWith("$") && StartsWith(workspaceRoot);

        /// <summary>
        /// Converts relative paths into absolute paths if needed.
        /// </summary>
        public static FilePath Create(string workspaceRoot, string path) =>
            path.StartsWith("$", StringComparison.OrdinalIgnoreCase)
                ? new FilePath(path)
                : System.IO.Path.IsPathRooted(path)
                    ? new FilePath(path)
                    : new FilePath(new FilePath(workspaceRoot) / path);
    }
}
