using System.Collections.Generic;
using System.Linq;
using Athena.Core.Domain;
using Athena.Core.UseCases;

namespace Athena.Adapters.DataAccess;

internal class BacklogRepository : IBacklogRepository
{
    private readonly IDatabase myDatabase;

    public BacklogRepository(IDatabase database)
    {
        myDatabase = database;
    }

    public IReadOnlyCollection<Improvement> GetBacklog()
    {
        return myDatabase.GetBacklog()
            .AsEnumerable()
            .Select(x => new Improvement(
                id: x.Id,
                title: x.Title,
                description: x.Description,
                iterationPath: x.IterationPath != null ? new IterationPath(x.IterationPath) : null,
                assignedTo: x.AssignedTo != null ? new EMail(x.AssignedTo) : null,
                workPackages: CreateWorkPackages(x.WorkPackages)
            ))
            .ToList();
    }

    private IReadOnlyList<WorkPackage> CreateWorkPackages(IList<WorkPackageDTO> workPackages) =>
        // TODO: implement
        null;
}
