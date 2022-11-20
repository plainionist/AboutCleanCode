using System;
using System.Collections.Generic;
using System.Linq;

namespace EventGuards;

public class ModelChangedEventArgs : EventArgs
{
    public ModelChangedEventArgs(string propertyName)
    {
        PropertyNames = new List<string> { propertyName };
    }

    public ModelChangedEventArgs(IEnumerable<string> propertyNames)
    {
        PropertyNames = propertyNames.ToList();
    }

    public IReadOnlyCollection<string> PropertyNames { get; }
}

