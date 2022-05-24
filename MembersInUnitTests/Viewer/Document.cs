using System;

namespace Viewer
{
    public class Document
    {
        public void Clear()
        {
            IsEmpty = true;
        }

        internal bool IsEmpty { get; private set;}
        public static Document Load(string v)
        {
            return new Document();
        }
    }
}