using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCode
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                throw new Exception($"Service of type {type} is already registered.");
            }

            _services[type] = service;
        }

        public static T Get<T>()
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            throw new Exception($"Service of type {type} is not registered.");
        }

        public static bool IsRegistered<T>()
        {
            return _services.ContainsKey(typeof(T));
        }

        public static void Unregister<T>()
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }

        public static void Clear()
        {
            Debug.Log("Clear ServiceLocator");
            _services?.Clear();
        }
    }
}