# RFC3339: a formatter and a parser

A very simple formatter and parser for RFC 3339 dates and times, working correctly with `DateTimeOffset`.

## Package

Available as a [![NuGet](https://img.shields.io/nuget/v/Rfc3339.svg?style=flat-square)](https://www.nuget.org/packages/Rfc3339) package.

## How to use it
```csharp
var dateTime = DateTime.Now;
var rfcDateTime = Rfc3339Formatter.Format(dateTime);

var dateTimeOffset = DateTimeOffset.Now;
var rfcDateTimeOffset = Rfc3339Formatter.Format(dateTimeOffset);

Rfc3339Parser.TryParse(rfcDateTime, out DateTimeOffset originalDateTimeOffset);
```
