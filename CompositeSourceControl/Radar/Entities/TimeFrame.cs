using System;

namespace Radar.Entities;

public record TimeFrame
{
    public TimeFrame(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public DateTime From { get; }
    public DateTime To { get; }

    public override string ToString() => $"[{From};{To}]";

    public static TimeFrame Empty { get; } = new TimeFrame(DateTime.MinValue, DateTime.MinValue);
}
