using UnityEngine;

namespace MyCode
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] protected PlatformType _platformType = PlatformType.Default;
        [SerializeField] protected bool _canMove = true;

        [field: SerializeField, HideInInspector] public Vector2Int GridIndex { get; private set; }

        public PlatformType PlatformType => _platformType;
        public bool CanMove => _canMove;

        public void SetupPlatform(Vector2Int gridIndex)
        {
            GridIndex = gridIndex;
        }

        public virtual void Event() { }
    }
}