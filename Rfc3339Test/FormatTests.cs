﻿using System;
using NUnit.Framework;
using Rfc3339;

namespace Rfc3339Test;

#pragma warning disable NUnit2005

[TestFixture]
public class FormatTests
{
    [Test]
    public void SimpleDateTimeTest()
    {
        var s = Rfc3339Formatter.Format(new DateTime(2000, 12, 02, 3, 4, 5, DateTimeKind.Utc), null);
        Assert.AreEqual("2000-12-02T03:04:05Z", s);
    }

    [Test]
    public void UnspecifiedSimpleDateTimeTest()
    {
        var s = Rfc3339Formatter.Format(new DateTime(2000, 12, 02, 3, 4, 5, DateTimeKind.Unspecified), null);
        Assert.AreEqual("2000-12-02T03:04:05", s);
    }

    [Test]
    public void SimpleDateTimeMsTest()
    {
        var s = Rfc3339Formatter.Format(new DateTime(2000, 12, 02, 3, 4, 5, DateTimeKind.Utc)
                                        + TimeSpan.FromMilliseconds(300), null);
        Assert.AreEqual("2000-12-02T03:04:05.300Z", s);
    }

    [Test]
    public void SimpleDateTimeμsTest()
    {
        var s = Rfc3339Formatter.Format(new DateTime(2000, 12, 02, 3, 4, 5, DateTimeKind.Utc)
                                        + TimeSpan.FromMilliseconds(0.9), null);
        Assert.AreEqual("2000-12-02T03:04:05.000900Z", s);
    }

    [Test]
    public void SimpleDateTimeNsTest()
    {
        var ts = TimeSpan.FromTicks(3);
        var s = Rfc3339Formatter.Format(new DateTime(2000, 12, 02, 3, 4, 5, DateTimeKind.Utc) + ts, null);
        Assert.AreEqual("2000-12-02T03:04:05.000000300Z", s);
    }

    [Test]
    public void NegativeTimeZoneTest()
    {
        var dateTimeOffset = new DateTimeOffset(2000, 12, 02, 3, 4, 5, -TimeSpan.FromHours(10));
        var s = Rfc3339Formatter.Format(dateTimeOffset, null);
        Assert.AreEqual("2000-12-02T03:04:05-10:00", s);
    }
}