using System.Collections.Generic;
using System.Xml;

namespace Depends.CLI
{
    internal class ProjectLoader : IProjectLoader
    {
        public ProjectLoader()
        {
        }

        public VsProject LoadProject(string path)
        {
            var propGroups = FindPropertyGroups(path);
            var assemblyName = GetAssemblyName(propGroups);
            return new VsProject()
            {
                Location = path,
                References = FindReferences(path),
                AssemblyName = assemblyName.InnerText
            };
        }

        public static IReadOnlyCollection<string> FindReferences(string csproj)
        {
            var doc = XmlUtils.LoadDocument(csproj);
            var root = doc.DocumentElement;

            var itemGroups = XmlUtils.FindChildNodes(root, "ItemGroup");
            return GetReferences(itemGroups, "Reference");
        }

        private static IReadOnlyCollection<string> GetReferences(
            IReadOnlyCollection<XmlNode> itemGroups, string childName)
        {
            var references = new List<string>();
            foreach (var node in itemGroups)
            {
                var children = XmlUtils.FindChildNodes(node as XmlElement, childName);
                foreach (var child in children)
                {
                    references.Add(child.Attributes["Include"].Value);
                }
            }
            return references;
        }

        private static IReadOnlyCollection<XmlNode> FindPropertyGroups(string csproj)
        {
            var doc = XmlUtils.LoadDocument(csproj);
            var root = doc.DocumentElement;

            return XmlUtils.FindChildNodes(root, "PropertyGroup");
        }

        private static XmlNode GetAssemblyName(IReadOnlyCollection<XmlNode> propGrpNodes)
        {
            foreach (var node in propGrpNodes)
            {
                var assemblyNameProperty = XmlUtils.FindChildNode(node, "AssemblyName");
                if (assemblyNameProperty != null)
                {
                    return assemblyNameProperty;
                }
            }
            return null;
        }
    }
}