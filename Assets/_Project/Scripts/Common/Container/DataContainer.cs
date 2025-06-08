using System.Collections.Generic;
using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Unity.VisualScripting;


namespace MyCode
{
    [Serializable]
    public class DataContainer
    {
        [SerializeField] private ContainerInfo[] _infos;

        private Dictionary<string, Object> _cacheDatas = new();

        public TReturn Get<TReturn>(string key) where TReturn : Object
        {
            if (_cacheDatas.ContainsKey(key))
                return (TReturn)_cacheDatas[key];

            foreach (var info in _infos)
            {
                if (info.Object.name.Equals(key))
                {
                    var findObject = info.Object.GetComponent<TReturn>();
                    if(info.IsCache)
                        _cacheDatas.Add(key, findObject);
                    return findObject;
                }
            }

            throw new Exception($"The desired object is missing in the data container: Key - {key}");
        }
    }

    [Serializable]
    public struct ContainerInfo
    {
        public Object Object;
        public bool IsCache;
    }
}