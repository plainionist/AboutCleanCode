using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AboutCleanCode
{
    public class Composer : IDisposable
    {
        private readonly Dictionary<Type, List<object>> mySingletons;

        public Composer(ILogger logger)
        {
            mySingletons = new Dictionary<Type, List<object>>();
        }

        public void Dispose()
        {
            foreach (var disposable in mySingletons.OfType<IDisposable>())
            {
                disposable.Dispose();
            }
        }

        public void AddSingleton<T>(T service)
        {
            if (!mySingletons.TryGetValue(typeof(T), out var instances))
            {
                instances = new List<object>();
                mySingletons.Add(typeof(T), instances);
            }
            instances.Add(service);
        }

        public T Resolve<T>()
        {
            if (!mySingletons.TryGetValue(typeof(T), out var instances))
            {
                throw new ArgumentException($"No instance found for type: {typeof(T)}");
            }

            if (instances.Count > 1)
            {
                throw new ArgumentException($"Multiple instance found for type: {typeof(T)}");
            }

            return (T)instances.Single();
        }

        public IEnumerable<T> ResolveMany<T>() =>
            mySingletons.TryGetValue(typeof(T), out var instances)
                ? instances.Cast<T>()
                : Enumerable.Empty<T>();

        public void Compose()
        {
            InjectExtensionPoints();
        }




        // 1. find all instances providing extension point (property decorated with [ExtensionPoint])
        // 2. find all instances assignable to this property type
        // 3. inject all found extensions into extension point
        private void InjectExtensionPoints()
        {
            foreach (var (type, instances) in mySingletons)
            {
                foreach (PropertyInfo extensionPoint in EnumerateExtensionPoints(type))
                {
                    Contract.Invariant(extensionPoint.PropertyType.IsGenericType
                        && typeof(IEnumerable).IsAssignableFrom(extensionPoint.PropertyType),
                        $"ExtensionPoint definition error: {extensionPoint} is not IEnumerable<T> or derived");

                    var setter = extensionPoint.GetSetMethod(true);

                    Contract.Invariant(setter != null,
                        $"ExtensionPoint definition error: {extensionPoint} does not have public or " +
                        "private setter (true readonly properties are not supported");

                    var typeArguments = extensionPoint.PropertyType.GetGenericArguments();

                    Contract.Invariant(typeArguments.Length == 1, 
                        "Only simple containers of single generic argument are supported");

                    var extensions = FindMatchingExtensions(typeArguments.Single());

                    foreach (var instance in instances)
                    {
                        setter.Invoke(instance, new[] { extensions });
                    }
                }
            }
        }








        // we currently only support Properties - injection into fields is considered 
        // to be a design smell as those field could not be set e.g. in tests without this
        // reflection magic here
        private IEnumerable<PropertyInfo> EnumerateExtensionPoints(Type type) =>
            type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.GetCustomAttribute<ExtensionPointAttribute>() != null);

        private IList FindMatchingExtensions(Type elementType)
        {
            // create List<> explicitly otherwise we cannot assign it to a property defined as generic type
            var extensions = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

            foreach (var instance in mySingletons.Where(x => elementType.IsAssignableFrom(x.Key)).SelectMany(x => x.Value))
            {
                extensions.Add(instance);
            }

            return extensions;
        }

    }
}
