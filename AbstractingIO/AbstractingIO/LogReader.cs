using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbstractingIO;

public partial class LogReader
{
    private static readonly Regex myLogPattern = new(@"^\[(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d) ([A-Z]+)\] ([a-zA-Z0-9\.]+): (.*)$", RegexOptions.Compiled);

    private LogMessage myLastMessage;

    public IReadOnlyCollection<LogMessage> ReadAllMessages(string file) =>
        ReadLines(file)
            .Select(Parse)
            .Where(x => x != null)
            .ToList();

    private static IEnumerable<string> ReadLines(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan);
        using var reader = new StreamReader(stream);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            yield return line;
        }
    }

    private LogMessage Parse(string line)
    {
        var msg = TryParse(line);
        if (msg != null)
        {
            myLastMessage = msg;
        }
        return msg;
    }

    private LogMessage TryParse(string line)
    {
        try
        {
            var enUS = new CultureInfo("en-US");
            var md = myLogPattern.Match(line);
            if (md.Success)
            {
                var dateStr = md.Groups[1].Value;
                var type = md.Groups[2].Value;
                var owner = md.Groups[3].Value;
                var msg = md.Groups[4].Value;
                if (DateTime.TryParse(dateStr, out var date))
                {
                    return new LogMessage(date, owner, type, msg);
                }
                else if (DateTime.TryParse(dateStr, enUS, DateTimeStyles.AssumeLocal, out date))
                {
                    return new LogMessage(date, owner, type, msg);
                }
                else
                {
                    return new LogMessage(DateTime.Now, owner, type, dateStr + "::" + msg);
                }
            }
            else
            {
                myLastMessage.AddMessage(line);
                return null;
            }
        }
        catch
        {
            return new LogMessage(DateTime.Now, nameof(LogReader), "ERROR", line);
        }
    }
}
