using System;
using System.Collections.Generic;

namespace TestFailureAnalyzer.Core.Defects
{
    public class DefectInput
    {
        public string Title { get; }
        public string Description { get; }
        public IReadOnlyCollection<Uri> Links{ get; }
    }
}
