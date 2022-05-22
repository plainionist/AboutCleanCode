using System.Collections.Generic;

namespace CodeOverComments
{
    public class ExceptionInfo
    {
        public string ExceptionType { get; internal set; }
        public string Message { get; internal set; }
        public IReadOnlyList<string> StackTrace { get; internal set; }
        public IReadOnlyList<ExceptionInfo> InnerExceptions { get; internal set; }
        public IReadOnlyCollection<CodeType> WordCloud { get; internal set; }
    }

    public class CodeType
    {
        public string FullName { get; set; }
        public int Occurrence { get; set; }
    }
}