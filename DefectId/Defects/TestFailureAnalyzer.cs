using System;

namespace Defects;

public class TestFailureAnalyzer
{
    private readonly DefectRepository myRepository;

    public TestFailureAnalyzer(DefectRepository repository)
    {
        myRepository = repository;
    }

    public void Analyze(TestCase testCase)
    {
        var defectId = myRepository.FindByTitle(testCase.Fullname);
        defectId.Match(
            id => UpdateDefectWithOccurrence(id, testCase),
            () => CreateNewDefect(testCase)
        );
    }

    private void UpdateDefectWithOccurrence(DefectId defectId, TestCase testCase)
    {
        throw new NotImplementedException();
    }

    private void CreateNewDefect(TestCase testCase)
    {
        throw new NotImplementedException();
    }
}
