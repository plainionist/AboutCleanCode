using System.Collections.Generic;
using System.Linq;
using Athena.Adapters.DataAccess;

namespace Athena.Adapters.TestApi;

internal class FakeDatabase : IDatabase
{
    private IReadOnlyCollection<ImprovementDTO> myWorkItems;

    public FakeDatabase(IReadOnlyCollection<ImprovementDTO> workItems)
    {
        myWorkItems = workItems;
    }

    public IQueryable<ImprovementDTO> GetBacklog() =>
        myWorkItems.AsQueryable();
}