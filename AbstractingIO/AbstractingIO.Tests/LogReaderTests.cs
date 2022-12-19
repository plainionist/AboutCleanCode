using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using NUnit.Framework;

namespace AbstractingIO.Tests;

[TestFixture]
class LogReaderTests
{
    [Test]
    public void ParsingLogLine()
    {
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>{
            { @"c:\temp\test.log", new MockFileData(string.Join(Environment.NewLine,
                "[2021-05-19 16:19:01 INF] Services.Analysis.WarmupCacheAgent: UpdateCache 00:00:27.2232923",
                "[2021-05-19 20:07:11 INF] Web.Startup: ConfigureServices"
            ))}
        });
        
        var reader = new LogReader(fileSystem);
        var msg = reader.ReadAllMessages(@"c:\temp\test.log").First();

        Assert.That(msg.Date, Is.EqualTo(new DateTime(2021, 5, 19, 16, 19, 1)));
        Assert.That(msg.MsgType, Is.EqualTo("INF"));
        Assert.That(msg.Owner, Is.EqualTo("Services.Analysis.WarmupCacheAgent"));
        Assert.That(msg.Message, Is.EqualTo("UpdateCache 00:00:27.2232923"));
    }
}
