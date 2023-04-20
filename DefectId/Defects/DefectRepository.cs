using System;

namespace Defects;

public record DefectId
{
    public DefectId(int value)
    {
        Contract.Requires(value > 0, "DefectId > 0");

        Value = value;
    }

    public int Value { get; }
}

public class DefectRepository
{
    /// <summary>
    /// ...
    /// </summary>
    public Option<DefectId> FindByTitle(string title)
    {
        var defect = Query($"Select * from ... where title = '{title}'");
        return defect != null
            ? Option.Some(new DefectId(defect.Id))
            : Option.None<DefectId>("not found");
    }

    /// <summary>
    /// ...
    /// </summary>
    public void Update(DefectId defectId, string title, string description)
    {
        Contract.RequiresNotNull(defectId);

        // ...
    }

    private DefectDto Query(string v)
    {
        throw new NotImplementedException();
    }
}
