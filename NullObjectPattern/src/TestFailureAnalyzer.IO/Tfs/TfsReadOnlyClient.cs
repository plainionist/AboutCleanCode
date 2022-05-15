
using System;
using TestFailureAnalyzer.Core.Defects;

namespace TestFailureAnalyzer.IO.Tfs
{
    public class TfsReadOnlyClient : IDefectRepository
    {
        private readonly IDefectRepository myImpl;

        public TfsReadOnlyClient(IDefectRepository impl)
        {
            myImpl = impl;
        }

        public IDefect Find(int id) =>
            myImpl.Find(id);

        public IDefect CreateDefect(DefectInput input) =>
            // return "dummy" adapter with fake id
            new WorkItemAdapter(input);

        public void UpdateDefect(IDefect defect)
        {
        }
    }
}
