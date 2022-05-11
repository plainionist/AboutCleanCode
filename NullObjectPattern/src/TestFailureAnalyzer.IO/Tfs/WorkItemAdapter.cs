using System;
using System.Collections.Generic;
using TestFailureAnalyzer.Core.Defects;

namespace TestFailureAnalyzer.IO.Tfs
{
    internal class WorkItemAdapter : IDefect
    {
        private DefectInput input;

        public WorkItemAdapter(DefectInput input)
        {
            this.input = input;
        }

        public WorkItemAdapter()
        {
        }

        public int Id => throw new NotImplementedException();

        public IDictionary<string, object> Fields => throw new NotImplementedException();

        public IList<Uri> Links => throw new NotImplementedException();
    }
}