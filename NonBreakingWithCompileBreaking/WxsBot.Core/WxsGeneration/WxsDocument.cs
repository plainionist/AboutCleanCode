
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WxsBot.Entities;

namespace WxsBot.WxsGeneration
{
    public class WxsDocument
    {
        private readonly HashSet<Assembly> myAssemblies;
        private readonly HashSet<ContentFile> myContentFiles;

        public WxsDocument(string path)
        {
            Contract.RequiresNotNullNotEmpty(path, nameof(path));

            WxsPath = path;
            WxsFileName = Path.GetFileNameWithoutExtension(path);

            myAssemblies = new HashSet<Assembly>();
            myContentFiles = new HashSet<ContentFile>();
        }

        public string WxsPath { get; }

        public string WxsFileName { get; }

        public IReadOnlyCollection<Assembly> Assemblies => myAssemblies;

        public IReadOnlyCollection<ContentFile> ContentFiles => myContentFiles;

        public bool IsEmpty => !myAssemblies.Any() && !myContentFiles.Any();

        public void Add(Assembly assembly)
        {
            myAssemblies.Add(assembly);
        }

        public void Add(ContentFile contentFile)
        {
            myContentFiles.Add(contentFile);
        }
    }
}