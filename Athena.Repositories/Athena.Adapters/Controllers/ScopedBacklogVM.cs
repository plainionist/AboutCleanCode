using System.Collections;
using System.Collections.Generic;

namespace Athena.Adapters.Controllers;

public class ScopedBacklogVM
{
    public IList<WorkItemVM> WorkItems { get; init; }
}