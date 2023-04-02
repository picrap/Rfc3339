using NUnit.Framework;
using Rfc3339;

namespace Rfc3339Test;

#pragma warning disable NUnit2005

[TestFixture]
public class ParseTests
{
    [Test]
    public void SimpleDateTest()
    {
        var ok = Rfc3339Parser.TryParse("2023-04-02", out Rfc3339DateTime d);
        Assert.IsTrue(ok);
        Assert.AreEqual(2023, d.Date.Year);
        Assert.AreEqual(4, d.Date.Month);
        Assert.AreEqual(2, d.Date.Day);
        Assert.IsNull(d.Time);
        Assert.IsNull(d.Offset);
    }

    [Test]
    [TestCase(" ")]
    [TestCase("T")]
    public void SimpleDateTimeTest(string separator)
    {
        var ok = Rfc3339Parser.TryParse($"2023-04-02{separator}14:06:49", out Rfc3339DateTime d);
        Assert.IsTrue(ok);
        Assert.AreEqual(2023, d.Date.Year);
        Assert.AreEqual(4, d.Date.Month);
        Assert.AreEqual(2, d.Date.Day);
        Assert.AreEqual(14, d.Time?.Hour);
        Assert.AreEqual(6, d.Time?.Minute);
        Assert.AreEqual(49, d.Time?.Second);
        Assert.AreEqual(0, d.Time?.Millisecond);
        Assert.IsNull(d.Offset);
    }

    [Test]
    [TestCase(" ")]
    [TestCase("T")]
    public void SimpleDateTimeFractionTest(string separator)
    {
        var ok = Rfc3339Parser.TryParse($"2023-04-02{separator}14:06:49.3", out Rfc3339DateTime d);
        Assert.IsTrue(ok);
        Assert.AreEqual(2023, d.Date.Year);
        Assert.AreEqual(4, d.Date.Month);
        Assert.AreEqual(2, d.Date.Day);
        Assert.AreEqual(14, d.Time?.Hour);
        Assert.AreEqual(6, d.Time?.Minute);
        Assert.AreEqual(49, d.Time?.Second);
        Assert.AreEqual(300, d.Time?.Millisecond);
        Assert.IsNull(d.Offset);
    }

    [Test]
    public void SimpleDateTimeOffsetTest()
    {
        var ok = Rfc3339Parser.TryParse($"2023-04-02T14:06:00+01:30", out Rfc3339DateTime d);
        Assert.IsTrue(ok);
        Assert.AreEqual(2023, d.Date.Year);
        Assert.AreEqual(4, d.Date.Month);
        Assert.AreEqual(2, d.Date.Day);
        Assert.AreEqual(14, d.Time?.Hour);
        Assert.AreEqual(6, d.Time?.Minute);
        Assert.AreEqual(0, d.Time?.Second);
        Assert.AreEqual(0, d.Time?.Millisecond);
        Assert.AreEqual(1, d.Offset?.Hours);
        Assert.AreEqual(30, d.Offset?.Minutes);
    }

    [Test]
    public void SimpleDateTimeNegativeOffsetTest()
    {
        var ok = Rfc3339Parser.TryParse($"2023-04-02T14:06:00-01:30", out Rfc3339DateTime d);
        Assert.IsTrue(ok);
        Assert.AreEqual(2023, d.Date.Year);
        Assert.AreEqual(4, d.Date.Month);
        Assert.AreEqual(2, d.Date.Day);
        Assert.AreEqual(14, d.Time?.Hour);
        Assert.AreEqual(6, d.Time?.Minute);
        Assert.AreEqual(0, d.Time?.Second);
        Assert.AreEqual(0, d.Time?.Millisecond);
        Assert.AreEqual(-1, d.Offset?.Hours);
        Assert.AreEqual(-30, d.Offset?.Minutes);
    }

    [Test]
    public void SimpleDateTimeZOffsetTest()
    {
        var ok = Rfc3339Parser.TryParse($"2023-04-02T14:06:00Z", out Rfc3339DateTime d);
        Assert.IsTrue(ok);
        Assert.AreEqual(2023, d.Date.Year);
        Assert.AreEqual(4, d.Date.Month);
        Assert.AreEqual(2, d.Date.Day);
        Assert.AreEqual(14, d.Time?.Hour);
        Assert.AreEqual(6, d.Time?.Minute);
        Assert.AreEqual(0, d.Time?.Second);
        Assert.AreEqual(0, d.Time?.Millisecond);
        Assert.AreEqual(0, d.Offset?.Hours);
        Assert.AreEqual(0, d.Offset?.Minutes);
    }

    [Test]
    public void SimpleDateTimeZeroOffsetTest()
    {
        var ok = Rfc3339Parser.TryParse($"2023-04-02T14:06:00+00:00", out Rfc3339DateTime d);
        Assert.IsTrue(ok);
        Assert.AreEqual(2023, d.Date.Year);
        Assert.AreEqual(4, d.Date.Month);
        Assert.AreEqual(2, d.Date.Day);
        Assert.AreEqual(14, d.Time?.Hour);
        Assert.AreEqual(6, d.Time?.Minute);
        Assert.AreEqual(0, d.Time?.Second);
        Assert.AreEqual(0, d.Time?.Millisecond);
        Assert.AreEqual(0, d.Offset?.Hours);
        Assert.AreEqual(0, d.Offset?.Minutes);
    }
}