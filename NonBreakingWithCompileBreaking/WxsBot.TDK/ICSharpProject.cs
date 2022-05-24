using System.Collections.Generic;

namespace WxsBot.TDK
{
    public interface ICSharpProject
    {
        string Name { get; }
        string Location { get; }
        string GetFullPath();
        List<ContentFile> ContentFiles { get; }

        ContentFile AddContentFile(string configPath, string fileName);
        void AddCSFile(string fileName);
        void DeleteContentFile(string fileName);
        void AddReference(string reference);
        void AddElementToPropertyGroup(string elementName);
        void RenameContentFile(string fileOldName, string fileNewName);
        void AddClassFile(string fileName);
    }
}