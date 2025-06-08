using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyCode
{
    public class Factory : MonoSingleton<Factory>, IFactory
    {
        [SerializeField] private DataContainer _container;

        [Inject] private IObjectResolver _resolver;

        public T CreateInjected<T>(string key, Vector3 position = default) where T : MonoBehaviour
        {
            var createObject = _resolver.Instantiate(_container.Get<T>(key));
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