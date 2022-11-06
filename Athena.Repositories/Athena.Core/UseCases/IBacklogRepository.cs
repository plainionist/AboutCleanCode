using System.Collections.Generic;
using Athena.Core.Domain;

namespace Athena.Core.UseCases;

public interface IBacklogRepository
{
    IReadOnlyCollection<Improvement> GetBacklog();
}