using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyCode
{
    [Serializable]
    public class MoodArray<T> where T : Object
    {
        [SerializeField] private T[] _array;

        public T GetValue(int index)
        {
            if(index < 0 || index >= _array.Length)
                return null;

            return _array[index];
        }

        public T Get<TReturn>(string key) where TReturn : T
        {
            return (TReturn)Get(key);
        }

        public T Get(string key)
        {
            foreach (var info in _array)
            {
                if (info.name.Equals(key))
                    return info;
            }

            throw new NullReferenceException($"The desired object is missing: Key - {key}");
        }
    }
}