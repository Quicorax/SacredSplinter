using System;
using System.Collections.Generic;

namespace Quicorax.SacredSplinter.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, IService> Services = new();

        public static void RegisterService<T>(T service) where T : class, IService
        {
            Services.Add(typeof(T), service);
        }

        public static T GetService<T>() where T : class, IService
        {
            if (!Services.TryGetValue(typeof(T), out var service))
                return default;

            return (T)service;
        }

        public static void UnregisterService<T>()
        {
            var type = typeof(T);

            if (Services.ContainsKey(type))
                Services.Remove(type);
        }
    }
}