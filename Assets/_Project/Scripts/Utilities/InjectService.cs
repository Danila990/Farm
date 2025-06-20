using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyCode
{
    public class InjectService : Singleton<InjectService>
    {
        [Inject] private IObjectResolver _container;
        
        public T CreateInject<T>(T prefab) where T : MonoBehaviour
        {
            T newPrefab = _container.Instantiate(prefab);
            return newPrefab;
        }

        public void Inject(GameObject prefab)
        {
            _container.InjectGameObject(prefab);
        }
    }
}