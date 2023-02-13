using Athena.Backlog.Adapters.TestApi;
using Athena.Backlog.IO.V1;
using Athena.Backlog.UseCases;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Athena.Backlog.Tests;

public class BacklogTests
{
    [Test]
    public void WithoutIteration()
    {
        var controller = new BacklogController(
            new BacklogInteractor(new FakeWorkItemRepository()),
            new FakeTeamsRepository());

        var response = controller.GetBacklog("TeamA", null);

        Assert.That(response, Is.AssignableFrom<OkObjectResult>());
        var vm = (BacklogResponseModel)((OkObjectResult)response).Value;
        Assert.That(vm.WorkItems, Is.Not.Empty);
    }
}
