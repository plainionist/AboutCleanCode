
using System;
using TestFailureAnalyzer.Core.Defects;

namespace TestFailureAnalyzer.IO.Tfs
{
    public class TfsClient : IDefectRepository
    {
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
                // 3. save the work item ...
                // 4. create adapter to not expose TFS APIs
                return new WorkItemAdapter(/* tfsWorkItem */ );
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
                // TODO: save the workitem back to TFS ...
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new defect: {defect.Id}", ex);
            }
        }
    }
}
