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
        if (defectId == -1)
        {
            CreateNewDefect(testCase);
        }
        else
        {
            UpdateDefectWithOccurrence(defectId, testCase);
        }
    }

    private void UpdateDefectWithOccurrence(int defectId, TestCase testCase)
    {
        throw new NotImplementedException();
    }

    private void CreateNewDefect(TestCase testCase)
    {
        throw new NotImplementedException();
    }
}
