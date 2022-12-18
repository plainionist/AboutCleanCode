using System;

namespace AbstractingIO;

public partial class LogReader
{
    public class LogMessage
    {
        public LogMessage(DateTime date, string owner, string type, string message)
            => (Date, Owner, MsgType, Message) = (date, owner, type, message);

        public DateTime Date { get; }
        public string Owner { get; }
        public string MsgType { get; }
        public string Message { get; private set; }

        internal void AddMessage(string text)
        {
            Message += text;
        }
    }
}
