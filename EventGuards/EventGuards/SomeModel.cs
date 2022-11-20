using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EventGuards;

public class SomeModel
{
    private string myValue1;
    private string myValue2;
    private string myValue3;

    public event EventHandler<ModelChangedEventArgs> Changed;

    public string Value1
    {
        get { return myValue1; }
        set { SetProperty(ref myValue1, value); }
    }

    public string Value2
    {
        get { return myValue2; }
        set { SetProperty(ref myValue2, value); }
    }

    public string Value3
    {
        get { return myValue3; }
        set { SetProperty(ref myValue3, value); }
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        Changed?.Invoke(this, new ModelChangedEventArgs(propertyName));

        return true;
    }
}

