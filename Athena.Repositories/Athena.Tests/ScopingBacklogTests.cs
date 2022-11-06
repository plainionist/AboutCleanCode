using System.Collections.Generic;
using Athena.Adapters.DataAccess;
using Athena.Adapters.TestApi;
using NUnit.Framework;

namespace Athena.Tests;

[TestFixture]
internal class ScopingBacklogTests
{
    [Test]
    public void EmptyBacklog_NothingScoped()
    {
        var testApi = new ScopeBacklogApi();

        // TODO: create backlog
        var workItems = new List<ImprovementDTO>();

        var vm = testApi.ScopeBacklog(workItems);

        Assert.That(vm.WorkItems, Is.Empty);
    }
}
