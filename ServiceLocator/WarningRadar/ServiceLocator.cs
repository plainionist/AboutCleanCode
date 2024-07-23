namespace WarningRadar;

public class ServiceLocator
{
    private readonly Dictionary<Type, object> myServices = [];

    public void Register<T>(T service)
    {
        if (myServices.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"An instance of type {typeof(T)} is already registered.");
        }

        myServices[typeof(T)] = service;
    }

    public T Resolve<T>()
    {
        if (myServices.TryGetValue(typeof(T), out object service))
        {
            return (T)service;
        }

        throw new InvalidOperationException($"No instance registered for type {typeof(T)}");
    }

    public T TryResolve<T>()
    {
        if (myServices.TryGetValue(typeof(T), out object obj))
        {
            return (T)obj;
        }

        return default;
    }
}
