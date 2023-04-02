﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace Rfc3339;

public class Rfc3339Parser
{
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
    }

    private static readonly Regex DateEx = new(
        @"^(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})([T\ ](?<hour>\d{2})\:(?<minute>\d{2})(\:(?<second>\d{2}(\.\d{1,9})?))?)?$",
        RegexOptions.None, TimeSpan.FromSeconds(2));

    public static bool TryParse(string? literal, out Rfc3339DateTime dateTime)
    {
        if (literal is null)
        {
            dateTime = default;
            return false;
        }

        var dateMatch = DateEx.Match(literal);
        if (!dateMatch.Success)
        {
            dateTime = default;
            return false;
        }

        dateTime = new Rfc3339DateTime();
        dateTime.Date = new DateOnly(GetInt(dateMatch, "year"), GetInt(dateMatch, "month"), GetInt(dateMatch, "day"));
        if (HasGroup(dateMatch, "hour"))
        {
            var resultTime = new TimeOnly(GetInt(dateMatch, "hour"), GetInt(dateMatch, "minute"));
            resultTime = resultTime.Add(TimeSpan.FromTicks((long)(GetDecimal(dateMatch, "second") * 10_000_000)));
            dateTime.Time = resultTime;
        }
        return true;
    }

    private static int GetInt(Match match, string groupName, int defaultValue = 0)
    {
        if (!HasGroup(match, groupName))
            return defaultValue;
        return int.Parse(match.Groups[groupName].Value, CultureInfo.InvariantCulture);
    }

    private static bool HasGroup(Match match, string groupName)
    {
        return match.Groups.TryGetValue(groupName, out var group) && !string.IsNullOrEmpty(group.Value);
    }

    private static decimal GetDecimal(Match match, string groupName, decimal defaultValue = 0)
    {
        if (!HasGroup(match, groupName))
            return defaultValue;
        var value = match.Groups[groupName].Value;
        if (value.StartsWith("."))
            value = "0" + value;
        return decimal.Parse(value, CultureInfo.InvariantCulture);
    }
}
