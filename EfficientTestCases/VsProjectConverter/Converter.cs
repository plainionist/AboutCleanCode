
using System;
using System.IO.Abstractions;
using System.Xml.Linq;

namespace VsProjectConverter
{
    public class Converter
    {
        private IFileSystem myFs;

        public Converter(IFileSystem fs)
        {
            this.myFs = fs;
        }

        public XElement Convert(string path)
        {
            throw new NotImplementedException();
        }
    }
}