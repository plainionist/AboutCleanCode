using System;

namespace EventGuards;

public class ModelChangedEventArgs : EventArgs
{
    public ModelChangedEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }

    public string PropertyName { get; }
}

