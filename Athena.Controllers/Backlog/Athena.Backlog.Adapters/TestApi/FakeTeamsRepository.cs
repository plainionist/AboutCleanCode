using Athena.Backlog.UseCases;

namespace Athena.Backlog.Adapters.TestApi;

public class FakeTeamsRepository : ITeamsRepository
{
    public Team TryFindByName(string name)
    {
        if (name == "TeamA")
        {
            return new Team
            {
                Name = "TeamA",
                AreaPath = @"Dev\Teams\A",
                Members = new[] {
                new Developer("Bob", "bob@company.com"),
                new Developer("Olivia", "oliver@company.com")
            }
            };
        }
        return null;
    }
}