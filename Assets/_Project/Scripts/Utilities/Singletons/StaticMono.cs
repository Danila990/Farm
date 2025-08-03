using UnityEngine;

namespace ProjectCode
{
    public abstract class StaticMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }

        protected void OnDestroy()
        {
            Instance = null;
            Destroy(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}