using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AbstractingIO.Tests;

[TestFixture]
class LogReaderTests
{
    [Test]
    public void ParsingLogLine()
    {
        var fileContent = string.Join(Environment.NewLine,
            "[2021-05-19 16:19:01 INF] Services.Analysis.WarmupCacheAgent: UpdateCache 00:00:27.2232923",
            "[2021-05-19 20:07:11 INF] Web.Startup: ConfigureServices"
        );
        using var stringReader = new StringReader(fileContent);
        
        var reader = new LogReader();
        var msg = reader.ReadAllMessages(stringReader).First();

        Assert.That(msg.Date, Is.EqualTo(new DateTime(2021, 5, 19, 16, 19, 1)));
        Assert.That(msg.MsgType, Is.EqualTo("INF"));
        Assert.That(msg.Owner, Is.EqualTo("Services.Analysis.WarmupCacheAgent"));
        Assert.That(msg.Message, Is.EqualTo("UpdateCache 00:00:27.2232923"));
    }
}
