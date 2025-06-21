using UnityEngine;


namespace MyCode
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private PlatformType _platformType = PlatformType.Default;
        [SerializeField] private bool _isCanMove = true;
        [SerializeField] private Vector2Int _gridIndex;

        public PlatformType PlatformType => _platformType;
        public bool IsCanMove => _isCanMove;
        public Vector2Int GridIndex => _gridIndex;

        public void SetupPlatform(Vector2Int gridIndex)
        {
            _gridIndex = gridIndex;
        }

        public virtual void Event()
        {

        }
    }
}