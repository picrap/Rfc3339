using System;
using NUnit.Framework;
using Rfc3339;

namespace Rfc3339Test;

[TestFixture]
public class ToStringTests
{
    [Test]
    public void SimpleDateTimeTest()
    {
        var s = Rfc3339Formatter.Format(new DateTime(2000, 12, 02, 3, 4, 5, DateTimeKind.Utc), null);
        Assert.AreEqual("2000-12-02T03:04:05Z", s);
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
}