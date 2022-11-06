using System.Collections.Generic;
using System.Linq;
using Athena.Core.Domain;
using Athena.Core.UseCases;

namespace Athena.Adapters.DataAccess;

internal class SqlBacklogRepository : IBacklogRepository
{
    private readonly ISqlDatabase myDatabase;

    public SqlBacklogRepository(ISqlDatabase database)
    {
        myDatabase = database;
    }

    public IReadOnlyCollection<Improvement> GetBacklog()
    {
        var workItems = myDatabase.Query("SELECT * FROM improvements WHERE ...");

        return workItems
            .Where(x => x.Fields["WorkItemType"].ToString() == "Improvement")
            .Select(x => new Improvement(
                id: x.Id,
                title: x.Fields["Title"].ToString(),
                description: x.Fields["Description"].ToString(),
                iterationPath: x.Fields["IterationPath"] != null ? new IterationPath(x.Fields["IterationPath"].ToString()) : null,
                assignedTo: x.Fields["AssignedTo"] != null ? new EMail(x.Fields["AssignedTo"].ToString()) : null,
                // TODO: add work packages
                workPackages: null
            ))
            .ToList();
    }

    public void CreateSchema()
    {
        myDatabase.Execute("CREATE TABLE Improvements { ... }");

        // TODO: create other tables
    }
}
