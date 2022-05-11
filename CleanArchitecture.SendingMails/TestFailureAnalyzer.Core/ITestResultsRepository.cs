namespace TestFailureAnalyzer
{
    public interface ITestResultsRepository
    {
        TestDetails GetTestDetails(int testCaseId);
    }

    public class TestDetails
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Assembly { get; init; }
    }
}