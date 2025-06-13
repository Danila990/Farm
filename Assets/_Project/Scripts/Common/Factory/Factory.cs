using UnityEngine;
using Zenject;

namespace MyCode
{
    public class Factory : MonoSingleton<Factory>, IFactory
    {
        [SerializeField] private DataContainer _container;

        [Inject] private DiContainer _diContainer;

        public T CreateInjected<T>(string key, Vector3 position = default) where T : MonoBehaviour
        {
            var createObject = _diContainer.InstantiatePrefabForComponent<T>(_container.Get<T>(key));
            createObject.transform.position = position;
            return createObject;
        }

        public T Create<T>(string key, Vector3 position = default) where T : MonoBehaviour
        {
            var createObject = Instantiate(_container.Get<T>(key));
            createObject.transform.position = position;
            return createObject;
        }
    }
}