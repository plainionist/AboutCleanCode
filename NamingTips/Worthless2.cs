using System;

namespace Naming
{
    public class PlannedTestCase
    {
        public PlannedTestCase(string id, string name, DateTime timestamp)
        {
            Id = id;
            Name = name;
            Timestamp = timestamp;
        }

        public string Id { get; }
        public string Name { get; }
        public DateTime Timestamp { get; }
    }
}
