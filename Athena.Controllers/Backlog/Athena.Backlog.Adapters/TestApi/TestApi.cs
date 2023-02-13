using Athena.Backlog.UseCases;

namespace Athena.Backlog.Adapters.TestApi;

public class TestApi
{
    public BacklogVM GetBacklog(string team, string iteration)
    {
        var adapter = new BacklogControllerAdapter(
            new BacklogInteractor(new FakeWorkItemRepository()),
            new FakeTeamsRepository());

        return adapter.GetBacklog(team, iteration);
    }
}
