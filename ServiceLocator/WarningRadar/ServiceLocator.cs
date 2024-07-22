namespace WarningRadar;

public class ServiceLocator
{
    private static ServiceLocator myInstance;
    private readonly Dictionary<Type, object> myServices = [];

    public static ServiceLocator Instance
    {
        get { return myInstance ??= new ServiceLocator(); }
    }

    public void Register<T>(T service)
    {
        myServices[typeof(T)] = service;
    }

    public T Resolve<T>()
    {
        if (myServices.TryGetValue(typeof(T), out object service))
        {
            return (T)service;
        }

        throw new InvalidOperationException($"No service registered for type {typeof(T)}");
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
