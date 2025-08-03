using UnityEngine;

namespace ProjectCode
{
    public abstract class SingletonMono<T> : StaticMono<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            base.Awake();
        }
    }
}