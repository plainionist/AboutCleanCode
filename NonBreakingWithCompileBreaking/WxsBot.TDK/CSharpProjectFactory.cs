
using System.Collections.Generic;
using System.Xml.Linq;

namespace WxsBot.TDK
{
    public class CSharpProjectFactory
    {
        private bool myUseSdkStyleProjects;

        public CSharpProjectFactory(bool useSdkStyleProjects)
        {
            myUseSdkStyleProjects = useSdkStyleProjects;
        }

        public ICSharpProject Create(string root, string name, bool isLibrary = false, List<XElement> referenceElements = null)
        {
            return myUseSdkStyleProjects
                ? SdkStyleCSharpProject.Create(root, name, isLibrary, referenceElements)
                : LegacyCSharpProject.Create(root, name, isLibrary, referenceElements);
        }
    }
}