using System.IO;
using System.Xml.Linq;
using WxsBot.Entities;

namespace WxsBot.TDK
{
    public class ContentFile
    {
        private ContentFile(ICSharpProject project, string configFilePath, string filName)
        {
            Project = project;
            ConfigLocation = configFilePath;
            FileName = filName;
        }

        public ICSharpProject Project { get; }

        public string ConfigLocation { get; private set; }

        public string FileName { get; private set; }

        public static ContentFile CreateConfigFile(ICSharpProject project, string configPath, string fileName)
        {
            var configFile = new XElement("Config",
                new XElement("FileName", fileName));
            configFile.Save(Path.Combine(project.Location, configPath, fileName));

            return new ContentFile(project, configPath, fileName);
        }

        public void RenameContentFile(string newName)
        {
            if (File.Exists(FileName))
            {
                File.Move(FileName, newName);
            }

            Project.RenameContentFile(FileName, newName);
            FileName = newName;
        }

        public void MoveContentFile(ICSharpProject targetProject, FilePath fileNewConfigPath)
        {
            if (!Directory.Exists(Path.Combine(targetProject.Location, fileNewConfigPath.ToString())))
            {
                Directory.CreateDirectory(Path.Combine(targetProject.Location, fileNewConfigPath.ToString()));
            }

            File.Move(Path.Combine(Project.Location, ConfigLocation, FileName), Path.Combine(targetProject.Location, fileNewConfigPath.ToString(), FileName));

            Project.DeleteContentFile(Path.Combine(Project.Location, ConfigLocation, FileName));

            targetProject.AddContentFile(fileNewConfigPath.ToString(), FileName);

            ConfigLocation = fileNewConfigPath.ToString();
        }
    }
}
