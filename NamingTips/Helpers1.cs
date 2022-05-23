
using System.Collections.Generic;

namespace Naming
{
    public class XmlDocument
    {
        private string myPath;

        public XmlDocument(string path) => myPath = path;

        public T ReadFromXml<T>() => default(T);

        public void WriteToXml<T>(T data) { }

        public static List<XmlContent> ReadPath(string path, string xPath) => null;

        public class XmlContent
        {
            public string Name { get; init; }
            public string Value { get; init; }
            public string Comment { get; init; }
        }
    }
}