using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectCode
{
    [Serializable]
    public class MoodArray<TData> where TData : Object
    {
        [SerializeField] private TData[] _array;

        public MoodArray(TData[] datas)
        {
            _array = datas;
        }

        public TReturn Get<TReturn>(string key) where TReturn : TData
        {
            return (TReturn)Get(key);
        }

        public TData Get(string key)
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