using System;

namespace ProjectCode
{
    public class StaticClass<T>where T : class, new()
    {
        private static Lazy<T> _instance = new Lazy<T>(() => new T());

        public static T Instance => _instance.Value;

        public static void Reset()
        {
            _instance = new Lazy<T>(() => new T());
        }
    } 
}