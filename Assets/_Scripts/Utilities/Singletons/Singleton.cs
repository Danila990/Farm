using UnityEngine;

namespace Code
{
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            base.Awake();
        }
    }
}