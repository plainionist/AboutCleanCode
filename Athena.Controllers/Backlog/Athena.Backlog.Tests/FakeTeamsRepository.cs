using Athena.Backlog.UseCases;

namespace Athena.Backlog.Tests;

internal class FakeTeamsRepository : ITeamsRepository
{
    public Team TryFindByName(string name)
    {
        if (name == "TeamA")
        {
            return new Team
            {
                Name = "TeamA",
                AreaPath = @"Dev\Teams\A",
                Members = new[] { "Bob", "Olivia" }
            };
        }
        return null;
    }
}