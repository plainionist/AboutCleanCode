
namespace AutoUnions.Inheritance;

public class UseCases
{
    public BurnDown ComputeBurnDown(IReadOnlyCollection<DevelopmentTeam> teams)
    {
        var workItems = GetWorkItemsOfTeams(teams);

        // TODO: implement

        return new BurnDown();
    }

    private object GetWorkItemsOfTeams(IReadOnlyCollection<DevelopmentTeam> teams)
    {
        throw new NotImplementedException();
    }

    public float GetRemainingCapacity(IReadOnlyCollection<AbstractTeam> teams)
    {
        var timeFrame = GetRemainingTimeFrame();

        return teams
            .OfType<DevelopmentTeam>()
            .Select(team => team.Capacity.Get(timeFrame))
            .Sum();
    }

    private (DateTime, DateTime) GetRemainingTimeFrame()
    {
        throw new NotImplementedException();
    }

    public float GetTotalCapacity(AbstractTeam team) =>
        team switch
        {
            DevelopmentTeam t => t.Capacity.Total,
            NonDevelopmentTeam t => 0,
            _ => throw new NotSupportedException($"Team type {team.GetType()}")
        };

}

public class BurnDown
{
}