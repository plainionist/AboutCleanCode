using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace VsProjectConverter.Tests
{
    [TestFixture]
    public class RemoveUnusedCode
    {
        private static readonly XNamespace MsBuild = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");

        private XElement CreateCsProject(params string[] codeFiles)
        {
            return new XElement(MsBuild + "Project",
                new XElement(MsBuild + "PropertyGroup",
                    new XElement(MsBuild + "AssemblyName", "Calculator"),
                    new XElement(MsBuild + "OutputType", "Exe")),
                new XElement(MsBuild + "ItemGroup",
                    codeFiles
                        .Select(x=>new XElement(MsBuild + "Compile", new XAttribute("Include", x)))),
                    // ,
                    // new XElement(MsBuild + "Compile", new XAttribute("Include", "b.cs"))),
                    // new XElement(MsBuild + "Compile", new XAttribute("Include", @"Impl\x.cs")),
                new XElement(MsBuild + "ItemGroup",
                    new XElement(MsBuild + "Reference", new XAttribute("Include", "System.Core")),
                    new XElement(MsBuild + "Reference", new XAttribute("Include", "System.Xml")))
            );
        }

        [Test]
        public void RemoveUnusedCodeFiles()
        {
            var csProject = CreateCsProject("a.cs", "b.cs");
            var fs = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\ws\a.cs", new MockFileData("some code") },
                { @"c:\ws\b.cs", new MockFileData("some code") },
                { @"c:\ws\c.cs", new MockFileData("some code") },
                { @"c:\ws\d.cs", new MockFileData("some code") },
                { @"c:\ws\Calculator.csproj", new MockFileData(csProject.ToString()) },
            });

            var converter = new Converter(fs);
            var convertedDoc = converter.Convert(@"c:\ws\Calculator.csproj");

            Assert.That(fs.File.Exists(@"c:\ws\a.cs"), Is.True);
            Assert.That(fs.File.Exists(@"c:\ws\b.cs"), Is.True);
            Assert.That(fs.File.Exists(@"c:\ws\c.cs"), Is.False);
            Assert.That(fs.File.Exists(@"c:\ws\d.cs"), Is.False);
        }

        [Test]
        public void RemoveUnusedCodeFilesFromFolders()
        {
            var csProject = CreateCsProject(@"Impl\x.cs");
            var fs = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\ws\Impl\x.cs", new MockFileData("some code") },
                { @"c:\ws\Impl\y.cs", new MockFileData("some code") },
                { @"c:\ws\Calculator.csproj", new MockFileData(csProject.ToString()) },
                { @"c:\ws\Calculator.csproj.user", new MockFileData("") },
                { @"c:\ws\Calculator.sln", new MockFileData("") },
            });

            var converter = new Converter(fs);
            var convertedDoc = converter.Convert(@"c:\ws\Calculator.csproj");

            Assert.That(convertedDoc.Elements("ItemGroup").Elements("Compile"), Is.Empty);

            Assert.That(fs.File.Exists(@"c:\ws\Impl\x.cs"), Is.True);
            Assert.That(fs.File.Exists(@"c:\ws\Impl\y.cs"), Is.False);
        }
    }
}
