
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Depends.CLI
{
    class XmlUtils
    {
        public static XmlDocument LoadDocument(string path)
        {
            var doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(path));
            return doc;
        }

        public static List<XmlNode> FindChildNodes(XmlElement root, string name)
        {
            List<XmlNode> nodes = new();
            foreach (var child in root.ChildNodes)
            {
                if (child is XmlElement childnode && childnode.Name == name)
                {
                    nodes.Add(childnode);
                }
            }

            return nodes;
        }

        public static XmlNode FindChildNode(XmlNode root, string name)
        {
            foreach (var child in root.ChildNodes)
            {
                if (child is XmlElement childnode && childnode.Name == name)
                {
                    return childnode;
                }
            }

            return null;
        }
    }
}
