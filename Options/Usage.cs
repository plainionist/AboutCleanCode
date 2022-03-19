using System;
using System.IO;

namespace AboutCleanCode
{
    public class MyComponent
    {
        public Option<FileInfo> GetFile(string path) =>
            File.Exists(path) ? Option.Some(new FileInfo(path)) : Option.None<FileInfo>($"No such file or directory: {path}");

        public void Print(Option<string> value) =>
            value.Match(
                x => Console.WriteLine(x),
                f => Console.Error.WriteLine($"Failed: {f}"));
    }
}