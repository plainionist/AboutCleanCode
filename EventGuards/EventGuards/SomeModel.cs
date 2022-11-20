using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EventGuards;

public class SomeModel
{
    private readonly List<ModelChangedGuard> myChangeGuards = new();
    private readonly List<string> myChangedProperties = new();

    private string myValue1;
    private string myValue2;
    private string myValue3;

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

    public event EventHandler<ModelChangedEventArgs> Changed;

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

        storage = value;
        if (myChangeGuards.Count == 0)
        {
            Changed?.Invoke(this, new ModelChangedEventArgs(propertyName));
        }
        else
        {
            myChangedProperties.Add(propertyName);
        }

        return true;
    }

    public IDisposable CreateChangedGuard()
    {
        var guard = new ModelChangedGuard(RemoveGuard);
        myChangeGuards.Add(guard);
        return guard;
    }

    private void RemoveGuard(ModelChangedGuard guard)
    {
        if (myChangeGuards[myChangeGuards.Count - 1] != guard)
        {
            throw new InvalidOperationException("ModelChangedGuard not used symmetrically!");
        }
        myChangeGuards.Remove(guard);

        if (myChangeGuards.Count == 0 && myChangedProperties.Count > 0)
        {
            Changed?.Invoke(this, new ModelChangedEventArgs(myChangedProperties));
            myChangedProperties.Clear();
        }
    }
}

