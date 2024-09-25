
namespace AutoUnions.Initial;

public class UseCases
{
    public BurnDown ComputeBurnDown(IReadOnlyCollection<Team> teams)
    {
        var devTeams = teams
            .Where(team => team.IsDevelopment)
            .ToList();

        var workItems = GetWorkItemsOfTeams(teams);

        // TODO: implement

        return new BurnDown();
    }

    private object GetWorkItemsOfTeams(IReadOnlyCollection<Team> teams)
    {
        throw new NotImplementedException();
    }

    public float GetRemainingCapacity(IReadOnlyCollection<Team> teams)
    {
        var timeFrame = GetRemainingTimeFrame();

        return teams
            .Select(team => team.Capacity)
            .Where(capacity => capacity != null)
            .Select(capacity => capacity!.Get(timeFrame))
            .Sum();
    }

    private (DateTime, DateTime) GetRemainingTimeFrame()
    {
        throw new NotImplementedException();
    }
}

public class BurnDown
{
}