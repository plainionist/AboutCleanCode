using System;
using System.Linq;
using NUnit.Framework;
using WxsBot.Entities;
using WxsBot.Gateways;
using WxsBot.TDK;

namespace WxsBot.Tests
{
    [TestFixture]
    public class SdkStyleVsProjectLoaderTests
    {
        private Workspace myWorkspace;

        [SetUp]
        public void Setup()
        {
            myWorkspace = Workspace.CreateFromTemplate();
        }

        [TestCase("Plainion.BE.Reporting.Core")]
        public void FilePath(string assemblyName)
        {
            myWorkspace.CreateBundle("Reporting", useSdkStyleProjects: true);
            myWorkspace.AddSourceLibraryProjects(assemblyName);

            var vsProject = TestApi.LoadVsProject(myWorkspace.Projects[assemblyName].GetFullPath());

            Assert.That(vsProject.FilePath, Is.EqualTo(myWorkspace.Projects[assemblyName].GetFullPath()));
        }

        [TestCase("Plainion.BE.Reporting.Core")]
        [TestCase("Plainion.FE.Reporting.Core")]
        [TestCase("Plainion.Common.Reporting.Core")]
        public void AssemblyName(string assemblyName)
        {
            myWorkspace.CreateBundle("Reporting", useSdkStyleProjects: true);
            myWorkspace.AddSourceLibraryProjects(assemblyName);

            var vsProject = TestApi.LoadVsProject(myWorkspace.Projects[assemblyName].GetFullPath());

            Assert.AreEqual(vsProject.Assembly.Name, assemblyName);

        }

        [TestCase("Plainion.BE.Reporting.Core")]
        [TestCase("Plainion.FE.Reporting.Core")]
        [TestCase("Plainion.Common.Reporting.Core")]
        public void AssemblyOutPutType_DLL(string assemblyName)
        {
            myWorkspace.CreateBundle("Reporting", useSdkStyleProjects: true);
            myWorkspace.AddSourceLibraryProjects(assemblyName);

            var vsProject = TestApi.LoadVsProject(myWorkspace.Projects[assemblyName].GetFullPath());

            Assert.AreEqual(vsProject.Assembly.OutputType, ProjectOutputType.Library);
        }

        [TestCase("Plainion.BE.Reporting.Core")]
        [TestCase("Plainion.FE.Reporting.Core")]
        [TestCase("Plainion.Common.Reporting.Core")]
        public void AssemblyOutPutType_EXE(string assemblyName)
        {
            myWorkspace.CreateBundle("Reporting", useSdkStyleProjects: true);
            myWorkspace.AddSourceExeProjects(assemblyName);

            var vsProject = TestApi.LoadVsProject(myWorkspace.Projects[assemblyName].GetFullPath());

            Assert.AreEqual(vsProject.Assembly.OutputType, ProjectOutputType.Executable);
        }

        [TestCase("Plainion.BE.Reporting.Core")]
        [TestCase("Plainion.FE.Reporting.Core")]
        [TestCase("Plainion.Common.Reporting.Core")]
        public void AssemblyFullName_DLL(string assemblyName)
        {
            myWorkspace.CreateBundle("Reporting", useSdkStyleProjects: true);
            myWorkspace.AddSourceLibraryProjects(assemblyName);

            var vsProject = TestApi.LoadVsProject(myWorkspace.Projects[assemblyName].GetFullPath());

            Assert.AreEqual(vsProject.Assembly.FullName, assemblyName + ProjectOutputType.Library);
        }

        [TestCase("Plainion.BE.Reporting.Core")]
        public void References(string assemblyName)
        {
            myWorkspace.CreateBundle("Reporting", useSdkStyleProjects: true);
            myWorkspace.AddSourceExeProjects(assemblyName);
            myWorkspace.Projects[assemblyName].AddReference("Plainion.BE.Rendering.Core");
            myWorkspace.Projects[assemblyName].AddReference("Plainion.BE.Rendering.Display");
            myWorkspace.Projects[assemblyName].AddReference("Plainion.BE.Rendering.Layouting");

            var vsProject = TestApi.LoadVsProject(myWorkspace.Projects[assemblyName].GetFullPath());

            Assert.That(vsProject.References, Is.Not.Empty);
            Assert.That(vsProject.References, Is.EquivalentTo(new string[] { "Plainion.BE.Rendering.Core", "Plainion.BE.Rendering.Display", "Plainion.BE.Rendering.Layouting" }));
        }

        [TestCase("Plainion.BE.Reporting.Core")]
        public void ContentFiles(string assemblyName)
        {
            myWorkspace.CreateBundle("Reporting", useSdkStyleProjects: true);
            myWorkspace.AddSourceLibraryProjects(assemblyName);
            myWorkspace.AddContentFile(assemblyName, (FilePath)"Config.DB", "Config.xml");

            var vsProject = TestApi.LoadVsProject(myWorkspace.Projects[assemblyName].GetFullPath());

            Assert.That(vsProject.ContentFiles.Any(r => r.Filename.Equals("Config.xml", StringComparison.OrdinalIgnoreCase)));
        }
    }
}
