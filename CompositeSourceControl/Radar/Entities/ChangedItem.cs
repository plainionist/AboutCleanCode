using System;

namespace Radar.Entities;

public record ChangedItem
{
    public ChangedItem(CodeBaseItem item, ChangeType changeType)
    {
        Contract.RequiresNotNull(item, nameof(item));

        Item = item;
        ChangeType = changeType;
    }

    public CodeBaseItem Item { get; }
    public ChangeType ChangeType { get; }
}

[Flags]
public enum ChangeType
{
    NotRelevant = 0,
    Add = 1,
    Edit = 2,
    Rename = 8,
    Delete = 16,
    Undelete = 32
}
