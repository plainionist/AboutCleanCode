namespace Athena.Backlog.Specs;

public class ComputingBacklog
{
    [Test]
    public void WhenWorkItemsExists_ThenBacklogShouldNotBeEmpty()
    {
        var testApi = new Backlog.TestApi.TestApi();

        var vm = testApi.GetBacklog("TeamA");

        Assert.That(vm.WorkItems, Is.Not.Empty);
    }
}