
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WxsBot.Entities;

namespace WxsBot.Gateways.MsBuild
{
    class LegacyVsProjectLoader : IVsProjectLoader
    {
        private static readonly XNamespace Namespace = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");

        public VsProject Load(string projectPath)
        {
            Contract.RequiresNotNullNotEmpty(projectPath, nameof(LegacyVsProjectLoader));

            if (!File.Exists(projectPath))
            {
                return null;
            }

            var projDoc = XDocument.Load(projectPath);
            var assemblyName = GetAssemblyNameForProject(projectPath, projDoc);
            var outputType = GetOutputTypeForProject(projDoc);
            var contentFiles = GetContentFilesForProject(projDoc, outputType, assemblyName);
            var referenceFiles = GetUsedReferencesForProject(projDoc);
            var isAssemblySerializable = IsAssemblySerializable(projDoc);

            return new VsProject(projectPath, assemblyName, contentFiles, referenceFiles, outputType, isAssemblySerializable);
        }

        private static string GetAssemblyNameForProject(string projectPath, XDocument projDoc)
        {
            if (projectPath.EndsWith(".vcxproj"))
            {
                var assemblyNodes = projDoc.Descendants(Namespace + "TargetName");
                if (assemblyNodes.Count() != 0)
                {
                    return assemblyNodes.First().Value;
                }

                assemblyNodes = projDoc.Descendants(Namespace + "ProjectName");
                if (assemblyNodes.Count() != 0)
                {
                    return assemblyNodes.First().Value;
                }

                throw new InvalidOperationException($"Assembly name not found: {projectPath}");
            }
            else
            {
                var assemblyNodes = projDoc.Descendants(Namespace + "AssemblyName");
                if (assemblyNodes.Count() != 0)
                {
                    return assemblyNodes.First().Value;
                }
                else
                {
                    throw new InvalidOperationException($"Assembly name not found: {projectPath}");
                }
            }
        }

        private static IEnumerable<string> GetContentFilesForProject(XDocument projDoc, ProjectOutputType outputType, string assemblyName)
        {
            var contentNodeLists = projDoc.Descendants(Namespace + "Content");
            var contentNodes = new List<string>();

            foreach (XElement contentNode in contentNodeLists)
            {
                if (contentNode.HasAttributes &&
                    contentNode.Attribute("Include") != null &&
                    contentNode.Attribute("Include").Value != null &&
                    contentNode.Elements(Namespace + "CopyToOutputDirectory").Any())
                {
                    contentNodes.Add(System.Net.WebUtility.UrlDecode(contentNode.Attribute("Include").Value));
                }
            }

            if (outputType == ProjectOutputType.Executable)
            {
                var configFile = GetConfigFilesForExecutableProject(projDoc, assemblyName);
                if (configFile != string.Empty)
                {
                    contentNodes.Add(configFile);
                }
            }
            return contentNodes;
        }

        private static string GetConfigFilesForExecutableProject(XDocument projDoc, string assemblyName)
        {
            var configNodeLists = projDoc.Descendants(Namespace + "None");

            foreach (XElement configFile in configNodeLists)
            {
                if (configFile.HasAttributes &&
                    configFile.Attribute("Include") != null &&
                    configFile.Attribute("Include").Value != null)
                {
                    if (configFile.Attribute("Include").Value.EndsWith(".config", StringComparison.OrdinalIgnoreCase))
                    {
                        return assemblyName + ".exe.config";
                    }
                }
            }
            return string.Empty;
        }

        private static IEnumerable<string> GetUsedReferencesForProject(XDocument projDoc)
        {
            return projDoc.Descendants(Namespace + "Reference")
                .Select(x => x.Attribute("Include").Value).ToList();
        }

        private static bool IsAssemblySerializable(XDocument ProjDoc)
        {
            var assemblySerializableNode = ProjDoc.Descendants(Namespace + "GenerateSerializationAssemblies");
            if (assemblySerializableNode.Count() > 0)
            {
                return true;
            }

            return false;
        }

        private static ProjectOutputType GetOutputTypeForProject(XDocument projDoc)
        {
            var projOutputTypeNode = projDoc.Descendants(Namespace + "OutputType");
            var csProjOutputType = projOutputTypeNode.Count() != 0 ? projOutputTypeNode.First().Value : null;
            if ("Library".Equals(csProjOutputType, StringComparison.InvariantCultureIgnoreCase))
            {
                return ProjectOutputType.Library;
            }

            if ("Exe".Equals(csProjOutputType, StringComparison.InvariantCultureIgnoreCase) ||
                "WinExe".Equals(csProjOutputType, StringComparison.InvariantCultureIgnoreCase))
            {
                return ProjectOutputType.Executable;
            }

            return ProjectOutputType.Library;
        }
    }
}
