namespace AutoUnions.SourceGen;

public class UseCases
{
    public BurnDown ComputeBurnDown(IReadOnlyCollection<Team> teams)
    {
        var devTeams = teams
            .Where(team => team.MatchDevelopmentTeam(x => true, () => false))
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
            .Select(team => team.MatchDevelopmentTeam(x => x.Capacity, () => null!))
            .Where(capacity => capacity != null)
            .Select(capacity => capacity!.Get(timeFrame))
            .Sum();
    }

    private (DateTime, DateTime) GetRemainingTimeFrame()
    {
        throw new NotImplementedException();
    }

    public float GetTotalCapacity(Team team) =>
        team.Match(
            static devTeam => devTeam.Capacity.Total,
            static nonDev => 0,
            static supplier => supplier.TotalCapacity
        );
}






public class BurnDown
{
}

