namespace AutoUnions.Generics;

public class UseCases
{
    public BurnDown ComputeBurnDown(IReadOnlyCollection<Team> teams)
    {
        var devTeams = teams
            .Where(team => team.IsT0)
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
            .Select(team => team.IsT0 ? team.AsT0.Capacity : null)
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

