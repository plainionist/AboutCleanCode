
using System;
using System.Collections.Generic;

namespace TestFailureAnalyzer.Core.Defects
{
    public interface IDefect
    {
        int Id { get; }
        IDictionary<string, object> Fields { get; }
        IList<Uri> Links { get; }
    }
}
