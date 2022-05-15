
using System;
using System.IO;
using TestFailureAnalyzer.Core.Defects;

namespace TestFailureAnalyzer.IO.Tfs
{
    public class TfsReadOnlyLoggingClient : IDefectRepository
    {
        private readonly IDefectRepository myImpl;
        private readonly TextWriter myLogger;

        public TfsReadOnlyLoggingClient(IDefectRepository impl, TextWriter logger)
        {
            myImpl = impl;
            myLogger = logger;
        }

        public IDefect Find(int id) =>
            myImpl.Find(id);

        public IDefect CreateDefect(DefectInput input)
        {
            // report to the operator that we would create a new defect for this failure
            myLogger.WriteLine($"Creating new defect: {input.Title}");
            myLogger.WriteLine($"  {input.Description}");

            // return "dummy" adapter with fake id
            return new WorkItemAdapter(input);
        }

        public void UpdateDefect(IDefect defect)
        {
            myLogger.WriteLine($"Updating defect: {defect.Id}");
        }
    }
}
