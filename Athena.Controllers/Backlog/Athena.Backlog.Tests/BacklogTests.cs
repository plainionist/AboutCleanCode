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
        var testApi = new TestApi();

        var vm = testApi.GetBacklog("TeamA", null);

        Assert.That(vm.WorkItems, Is.Not.Empty);
    }
}
