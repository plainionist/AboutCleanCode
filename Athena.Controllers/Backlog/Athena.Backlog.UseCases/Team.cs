using System.Collections.Generic;

namespace Athena.Backlog.UseCases
{
    public class Team
    {
        public string Name { get; init; }
        public string AreaPath { get; init; }
        public IReadOnlyCollection<Developer> Members { get; init; }
    }
}