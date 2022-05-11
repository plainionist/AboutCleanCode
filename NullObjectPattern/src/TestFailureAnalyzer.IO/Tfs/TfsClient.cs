
using System;
using TestFailureAnalyzer.Core.Defects;

namespace TestFailureAnalyzer.IO.Tfs
{
    public class TfsClient : IDefectRepository
    {
        private readonly bool myIsDryRun;

        public TfsClient(bool isDryRun)
        {
            myIsDryRun = isDryRun;
        }

        public IDefect Find(int id)
        {
            throw new NotImplementedException();
        }

        public IDefect CreateDefect(DefectInput input)
        {
            try
            {
                // 1. call TFS to create a work item of type "defect"
                // 2. copy all information from input into work item fields

                if (myIsDryRun)
                {
                    // 3. return "dummy" adapter with fake id
                    return new WorkItemAdapter(input);
                }
                else
                {
                    // 3. save the work item ...

                    // 4. create adapter to not expose TFS APIs
                    return new WorkItemAdapter(/* tfsWorkItem */ );
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new defect: {input.Title}", ex);
            }
        }

        public void UpdateDefect(IDefect defect)
        {
            try
            {
                if (myIsDryRun)
                {
                    // nothing to do
                    return;
                }

                // TODO: save the workitem back to TFS ...
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new defect: {defect.Id}", ex);
            }
        }
    }
}
