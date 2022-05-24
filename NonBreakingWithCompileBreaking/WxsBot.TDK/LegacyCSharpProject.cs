using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WxsBot.Entities;

namespace WxsBot.TDK
{
    public class LegacyCSharpProject : ICSharpProject
    {
        private static readonly XNamespace NS = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");

        private LegacyCSharpProject(string root, string name, bool isLibrary, List<XElement> referenceElements)
        {
            Name = name;
            Location = Path.Combine(root, Name);
            ContentFiles = new List<ContentFile>();

            var doc = new XElement(NS + "Project",
                new XElement(NS + "PropertyGroup",
                    new XElement(NS + "AssemblyName", name),
                    new XElement(NS + "ProjectGuid", Guid.NewGuid()),
                    new XElement(NS + "OutputType", isLibrary ? "Library" : "Exe")),
                CreateReferenceItemGroup(NS, referenceElements), 
                CreateAppConfigItemGroup(NS, !isLibrary));
            doc.Save(Path.Combine(root, name, name + ".csproj"));
        }

        private static XElement CreateReferenceItemGroup(XNamespace ns, List<XElement> referenceElements)
        {
            if (referenceElements != null)
            {
                var itemGroupElement = new XElement(ns + "ItemGroup");
                foreach (XElement element in referenceElements)
                {
                    itemGroupElement.Add(element);
                }
                return itemGroupElement;
            }

            return new XElement(ns + "ItemGroup");
        }

        private static XElement CreateAppConfigItemGroup(XNamespace ns, bool isAppConfigRequired)
        {
            var itemGroupElement = new XElement(ns + "ItemGroup");
            if (isAppConfigRequired)
            {
                itemGroupElement.Add(new XElement(ns + "None", new XAttribute("Include", "App.Config")));
            }
            return itemGroupElement;
        }

        public string Name { get; }

        public string Location { get; }

        public string GetFullPath() =>
            Path.Combine(Location, Name + ".csproj");

        public List<ContentFile> ContentFiles { get; }

        public static ICSharpProject Create(string root, string name, bool isLibrary = false, List<XElement> referenceElements = null)
        {
            Directory.CreateDirectory(Path.Combine(root, name));

            return new LegacyCSharpProject(root, name, isLibrary, referenceElements);
        }

        public ContentFile AddContentFile(string configPath, string fileName)
        {
            Directory.CreateDirectory(Path.Combine(Location, configPath));

            var contentFile = ContentFile.CreateConfigFile(this, configPath, fileName);

            AddContentFile((FilePath)configPath / fileName);

            ContentFiles.Add(contentFile);

            return contentFile;
        }

        public void AddCSFile(string fileName)
        {
            if (!File.Exists(Path.Combine(Location, fileName)))
            {
                File.Create(Path.Combine(Location, fileName)).Close();
            }

            AddClassFile(fileName);
        }

        public void DeleteContentFile(string fileName)
        {
            DeleteFile(fileName);
        }

        public void AddReference(string reference)
        {
            AddNewReference(reference);
        }

        public void AddElementToPropertyGroup(string elementName)
        {
            AddPropertyGroupNode(elementName);
        }

        public void RenameContentFile(string fileOldName, string fileNewName)
        {
            var doc = XDocument.Load(GetFullPath());

            var item = doc.Element(NS + "Project")
                .Elements(NS + "ItemGroup")
                .Elements(NS + "Content")
                .First(x => x.Attribute("Include").Value.Contains(Path.GetFileName(fileOldName)));

            item.Attribute("Include").Value = item.Attribute("Include").Value.Replace(Path.GetFileName(fileOldName), Path.GetFileName(fileNewName));

            doc.Save(GetFullPath());
        }

        private void AddContentFile(FilePath target)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Elements(NS + "ItemGroup").First().Add(
                new XElement(NS + "Content",
                    new XAttribute("Include", target.ToString().Replace(Location, "").TrimStart(Path.DirectorySeparatorChar)),
                    new XElement(NS + "CopyToOutputDirectory", "PreserveNewest")));

            doc.Save(GetFullPath());
        }

        public void AddClassFile(string fileName)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Elements(NS + "ItemGroup").First().Add(
                new XElement(NS + "Compile",
                    new XAttribute("Include", fileName)));

            doc.Save(GetFullPath());
        }

        private void DeleteFile(string fileToBeDeleted)
        {
            var doc = XDocument.Load(GetFullPath());

            var itemGroups = doc.Element(NS + "Project")
                .Elements(NS + "ItemGroup")
                .Elements(NS + "Content")
                .Where(x => x.Attribute("Include").Value.Contains(Path.GetFileName(fileToBeDeleted)))
                .ToList();

            itemGroups.Remove();

            doc.Save(GetFullPath());
        }

        private void AddNewReference(string reference)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Elements(NS + "ItemGroup").First().Add(
                new XElement(NS + "Reference",
                    new XAttribute("Include", reference)));

            doc.Save(GetFullPath());
        }

        private void AddPropertyGroupNode(string elementName)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Add(new XElement(NS + "ProprtyGroup",
                new XElement(NS + elementName, "DefaultValue")));
            doc.Save(GetFullPath());
        }
    }
}
