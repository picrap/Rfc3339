using System.Text;

namespace Rfc3339;

public class Rfc3339Formatter : IFormatProvider, ICustomFormatter
{
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        return arg switch
        {
            null => "",
            DateTime dateTime => Format(dateTime, format),
            DateTimeOffset dateTimeOffset => Format(dateTimeOffset, format),
            _ => throw new NotSupportedException($"Can’t format type {arg.GetType()}")
        };
    }

    public object? GetFormat(Type? formatType)
    {
        if (formatType == typeof(DateTime) || formatType == typeof(DateTime?)
                                           || formatType == typeof(DateTimeOffset) || formatType == typeof(DateTimeOffset?))
            return this;
        return null;
    }

    public static string Format(DateTime dateTime, string? format)
    {
        return Format(dateTime, DateTimeOffset.Now.Offset, format);
    }

    public static string Format(DateTimeOffset dateTime, string? format)
    {
        return Format(dateTime.DateTime, dateTime.Offset, format);
    }

    private static string Format(DateTime dateTime, TimeSpan offset, string? format = null)
    {
        var builder = new StringBuilder();
        builder.AppendFormat("{0:yyyy-MM-ddThh:mm:ss}", dateTime);
        var totalNs = dateTime.Ticks % 10_000_000 * 100;
        if (totalNs > 0)
        {
            builder.AppendFormat(".{0:000}", totalNs / 1_000_000);
            var μs = totalNs % 1_000_000;
            if (μs > 0)
            {
                builder.AppendFormat("{0:000}", μs / 1_000);
                var ns = μs % 1000;
                if (ns > 0)
                    builder.AppendFormat("{0:000}", ns);
            }
        }

        switch (dateTime.Kind)
        {
            case DateTimeKind.Unspecified:
                break;
            case DateTimeKind.Utc:
                builder.Append("Z");
                break;
            case DateTimeKind.Local:
                if (offset < TimeSpan.Zero)
                    builder.AppendFormat("-{0:hh:mm}", -offset);
                else
                    builder.AppendFormat("+{0:hh:mm}", offset);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return builder.ToString();
    }
}