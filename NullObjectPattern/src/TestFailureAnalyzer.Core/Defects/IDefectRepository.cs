
namespace TestFailureAnalyzer.Core.Defects
{
    public interface IDefectRepository
    {
        IDefect Find(int id);
        IDefect CreateDefect(DefectInput input);
        void UpdateDefect(IDefect defect);
    }
}