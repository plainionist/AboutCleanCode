using System.Collections.Generic;
using System.Linq;
using Athena.Core.Domain;
using Athena.Core.UseCases;

namespace Athena.Adapters.DataAccess;

internal class WiqlBacklogRepository : IBacklogRepository
{
    private readonly ISqlDatabase myWorkItemStore;

    public WiqlBacklogRepository(ISqlDatabase workItemStore)
    {
        myWorkItemStore = workItemStore;
    }

    public IReadOnlyCollection<Improvement> GetBacklog()
    {
        var workItems = myWorkItemStore.Query("SELECT * FROM WorkItemLinks WHERE ...");

        return workItems
            .Where(x => x.Fields["WorkItemType"].ToString() == "Improvement")
            .Select(x => new Improvement(
                id: x.Id.Value,
                title: x.Fields["Title"].ToString(),
                description: x.Fields["Description"].ToString(),
                iterationPath: x.Fields["IterationPath"] != null ? new IterationPath(x.Fields["IterationPath"].ToString()) : null,
                assignedTo: x.Fields["AssignedTo"] != null ? new EMail(x.Fields["AssignedTo"].ToString()) : null,
                // TODO: add work packages
                workPackages: null
            ))
            .ToList();
    }
}
