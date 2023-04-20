using System;

namespace Defects;

public class DefectRepository
{
    /// <summary>
    /// ...
    /// </summary>
    /// <returns>-1 if the defect was not found</returns>
    public int FindByTitle(string title)
    {
        var defect = Query($"Select * from ... where title = '{title}'");
        return defect != null
            ? defect.Id
            : -1;
    }

    /// <summary>
    /// ...
    /// </summary>
    /// <param name="defectId">expected to be a valid defect id</param>
    public void Update(int defectId, string title, string description)
    {
        Contract.Requires(defectId > 0, "DefectId > 0");

        // ...
    }

    private DefectDto Query(string v)
    {
        throw new NotImplementedException();
    }
}
