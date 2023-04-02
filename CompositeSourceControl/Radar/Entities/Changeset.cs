using System;
using System.Collections.Generic;

namespace Radar.Entities;

public abstract record SourceControlId
{
    public T Select<T>(Func<ChangesetId, T> case1, Func<CommitId, T> case2) =>
        this switch
        {
            ChangesetId x => case1(x),
            CommitId x => case2(x),
            _ => throw new NotSupportedException($"Unknown id type: {GetType()}")
        };
}

public record ChangesetId : SourceControlId
{
    public ChangesetId(int value)
    {
        Contract.Requires(value > 0, "Changeset id is expected to be > 0");
        Value = value;
    }

    public int Value { get; }
}

public record CommitId : SourceControlId
{
    public CommitId(string value)
    {
        Contract.RequiresNotNullNotEmpty(value, nameof(value));
        Value = value;
    }

    public string Value { get; }
}

public class Changeset
{
    public Changeset(SourceControlId id, DateTime createdAt, string comment, IReadOnlyCollection<ChangedItem> changes, CodeBase codeBase)
    {
        Contract.RequiresNotNull(changes, nameof(changes));

        Id = id;
        CreatedAt = createdAt;
        Comment = comment;
        Changes = changes;
        CodeBase = codeBase;
    }

    public CodeBase CodeBase { get; }
    public SourceControlId Id { get; }

    public IReadOnlyCollection<ChangedItem> Changes { get; }
    public string Comment { get; }
    public DateTime CreatedAt { get; }
}
