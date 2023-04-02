using System;
using System.Collections.Generic;

namespace Radar.Entities;

public class Changeset 
{
    public Changeset(int id, DateTime createdAt, string comment, IReadOnlyCollection<ChangedItem> changes, CodeBase codeBase)
    {
        Contract.RequiresNotNull(changes, nameof(changes));

        Id = id;
        CreatedAt = createdAt;
        Comment = comment;
        Changes = changes;
        CodeBase = codeBase;
    }

    public CodeBase CodeBase { get; }
    public int Id { get; }

    public IReadOnlyCollection<ChangedItem> Changes { get; }
    public string Comment { get; }
    public DateTime CreatedAt { get; }
}
