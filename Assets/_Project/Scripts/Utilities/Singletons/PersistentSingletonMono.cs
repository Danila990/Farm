using UnityEngine;

namespace ProjectCode
{
    public abstract class PersistentSingletonMono<T> : SingletonMono<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}