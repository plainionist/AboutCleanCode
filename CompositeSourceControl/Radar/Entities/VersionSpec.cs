using System;

namespace Radar.Entities;

public abstract record VersionSpec
{
    public void Match(Action<ChangesetVersionSpec> onChangesetSpec, Action<DateTimeVersionSpec> onDateTimeSpec)
    {
        switch (this)
        {
            case ChangesetVersionSpec x: onChangesetSpec(x); break;
            case DateTimeVersionSpec x: onDateTimeSpec(x); break;
            default: throw new NotSupportedException($"Unknown VersionSpec type: {GetType()}");
        };
    }

    public T Select<T>(Func<ChangesetVersionSpec, T> onChangesetSpec, Func<DateTimeVersionSpec, T> onDateTimeSpec) =>
        this switch
        {
            ChangesetVersionSpec x => onChangesetSpec(x),
            DateTimeVersionSpec x => onDateTimeSpec(x),
            _ => throw new NotSupportedException($"Unknown VersionSpec type: {GetType()}")
        };
}

public record ChangesetVersionSpec : VersionSpec
{
    public ChangesetVersionSpec(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public record DateTimeVersionSpec : VersionSpec
{
    public DateTimeVersionSpec(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; }
}
