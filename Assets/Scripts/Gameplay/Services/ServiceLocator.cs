using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, IService> _services = new();

    public static void RegisterService<T>(T service) where T : class, IService
    {
        _services.Add(typeof(T), service);
    }

    public static T GetService<T>() where T : class, IService
    {
        if (!_services.TryGetValue(typeof(T), out IService service))
            return default;

        return (T)service;
    } 

    public static void UnregisterService<T>()
    {
        Type type = typeof(T);

        if (_services.ContainsKey(type))
            _services.Remove(type);
    }
}
