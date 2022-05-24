using System.Collections.Generic;
using System.Linq;

namespace WxsBot.Entities
{
    public class VsProject
    {
        public VsProject(string path, string assemblyName, IEnumerable<string> contentFiles, IEnumerable<string> references, ProjectOutputType outputType, bool isAssemblySerializable)
        {
            FilePath = path;
            Assembly = new Assembly(assemblyName, outputType);
            ContentFiles = contentFiles
                .Select(f => new ContentFile(this, f))
                .ToList();
            References = references;
            IsAssemblySerializable = isAssemblySerializable;
        }

        public string FilePath { get; }
        public Assembly Assembly { get; }
        public IEnumerable<ContentFile> ContentFiles { get; }
        public IEnumerable<string> References { get; }
        public bool IsAssemblySerializable { get; }
    }
}
