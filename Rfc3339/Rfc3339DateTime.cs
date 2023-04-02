using System;

namespace Rfc3339;

public struct Rfc3339DateTime
{
    public DateOnly Date { get; internal set; }
    public TimeOnly? Time { get; internal set; }
    public TimeSpan? Offset { get; internal set; }

    public DateTimeOffset DateTimeOffset
    {
        get
        {
            var time = Time ?? TimeOnly.MinValue;
            var offset = Offset ?? TimeSpan.Zero;
            return new DateTimeOffset(Date.ToDateTime(time), offset);
        }
    }

    public DateTime DateTime
    {
        get
        {
            var time = Time ?? TimeOnly.MinValue;
            if (!Offset.HasValue)
                return Date.ToDateTime(time, DateTimeKind.Unspecified);
            if (Offset == TimeSpan.Zero)
                return Date.ToDateTime(time, DateTimeKind.Utc);
            return Date.ToDateTime(time, DateTimeKind.Unspecified);
        }
    }
}