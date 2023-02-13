using Athena.Backlog.Adapters.TestApi;
using Athena.Backlog.UseCases;
using NUnit.Framework;

namespace Athena.Backlog.Tests;

public class BacklogInteractorTests
{
    [Test]
    public void WithoutIteration()
    {
        var interactor = new BacklogInteractor(new FakeWorkItemRepository());
        var team = new FakeTeamsRepository().TryFindByName("TeamA");

        var response = interactor.GetBacklog(new BacklogRequestModel { Team = team });

        Assert.That(response.WorkItems, Is.Not.Empty);
    }
}
