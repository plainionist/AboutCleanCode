using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WxsBot.Entities;

namespace WxsBot.TDK
{
    public class SdkStyleCSharpProject : ICSharpProject
    {
        private SdkStyleCSharpProject(string root, string name, bool isLibrary, List<XElement> referenceElements)
        {
            Name = name;
            Location = Path.Combine(root, Name);
            ContentFiles = new List<ContentFile>();

            var doc = new XElement("Project",
                new XAttribute("Sdk", "Microsoft.NET.Sdk"),
                new XElement("PropertyGroup",
                    new XElement("AssemblyName", name),
                    new XElement("ProjectGuid", Guid.NewGuid()),
                    new XElement("OutputType", isLibrary ? "Library" : "Exe")),
                CreateReferenceItemGroup(referenceElements), 
                CreateAppConfigItemGroup(!isLibrary));
            doc.Save(Path.Combine(root, name, name + ".csproj"));
        }

        private static XElement CreateReferenceItemGroup(List<XElement> referenceElements)
        {
            if (referenceElements != null)
            {
                var itemGroupElement = new XElement("ItemGroup");
                foreach (XElement element in referenceElements)
                {
                    itemGroupElement.Add(element);
                }
                return itemGroupElement;
            }

            return new XElement("ItemGroup");
        }

        private static XElement CreateAppConfigItemGroup(bool isAppConfigRequired)
        {
            var itemGroupElement = new XElement("ItemGroup");
            if (isAppConfigRequired)
            {
                itemGroupElement.Add(new XElement("None", new XAttribute("Include", "App.Config")));
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

            return new SdkStyleCSharpProject(root, name, isLibrary, referenceElements);
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

            var item = doc.Element("Project")
                .Elements("ItemGroup")
                .Elements("Content")
                .First(x => x.Attribute("Include").Value.Contains(Path.GetFileName(fileOldName)));

            item.Attribute("Include").Value = item.Attribute("Include").Value.Replace(Path.GetFileName(fileOldName), Path.GetFileName(fileNewName));

            doc.Save(GetFullPath());
        }

        private void AddContentFile(FilePath target)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Elements("ItemGroup").First().Add(
                new XElement("Content",
                    new XAttribute("Include", target.ToString().Replace(Location, "").TrimStart(Path.DirectorySeparatorChar)),
                    new XElement("CopyToOutputDirectory", "PreserveNewest")));

            doc.Save(GetFullPath());
        }

        public void AddClassFile(string fileName)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Elements("ItemGroup").First().Add(
                new XElement("Compile",
                    new XAttribute("Include", fileName)));

            doc.Save(GetFullPath());
        }

        private void DeleteFile(string fileToBeDeleted)
        {
            var doc = XDocument.Load(GetFullPath());

            var itemGroups = doc.Element("Project")
                .Elements("ItemGroup")
                .Elements("Content")
                .Where(x => x.Attribute("Include").Value.Contains(Path.GetFileName(fileToBeDeleted)))
                .ToList();

            itemGroups.Remove();

            doc.Save(GetFullPath());
        }

        private void AddNewReference(string reference)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Elements("ItemGroup").First().Add(
                new XElement("Reference",
                    new XAttribute("Include", reference)));

            doc.Save(GetFullPath());
        }

        private void AddPropertyGroupNode(string elementName)
        {
            var doc = XDocument.Load(GetFullPath());

            doc.Root.Add(new XElement("ProprtyGroup",
                new XElement(elementName, "DefaultValue")));
            doc.Save(GetFullPath());
        }
    }
}
