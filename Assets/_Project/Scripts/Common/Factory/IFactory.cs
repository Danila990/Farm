using UnityEngine;


namespace MyCode
{
    public interface IFactory
    {
        public T Create<T>(string key, Vector3 position = default) where T : MonoBehaviour;
        public T CreateInjected<T>(string key, Vector3 position = default) where T : MonoBehaviour;
    }
}