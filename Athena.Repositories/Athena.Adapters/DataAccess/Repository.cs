using System;
using System.Collections.Generic;
using Athena.Core.Domain;
using Athena.Core.UseCases;

namespace Athena.Adapters.DataAccess;

internal class Repository : IBacklogRepository
{
    public IReadOnlyCollection<Improvement> GetBacklog()
    {
        // TODO: implement
        
        throw new NotImplementedException();
    }
}
