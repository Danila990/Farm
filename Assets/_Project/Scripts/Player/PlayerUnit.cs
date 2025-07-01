using UnityEngine;


namespace MyCode
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private MoveUnitComponent _moveUnit;
        [SerializeField] private RotateUnitComponent _rotateUnit;
        [SerializeField] private HealthUnitComponent _healthUnit;

        public Vector2Int GridIndex { get; private set; }

        private IGridMap _map;
        private bool _isActive = false;

        private void OnEnable()
        {
            EventBus.Subscribe<IGridMap>(OnUpdateGridMap);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<IGridMap>(OnUpdateGridMap);
        }

        public void Startable()
        {
            _isActive = true;
        }

        private void Update()
        {
            if (_moveUnit.IsMoved || !_isActive)
                return;

            DirectionType nextDirection = GetDirection();
            if (nextDirection == DirectionType.None)
                return;

            _rotateUnit.StartRotate(nextDirection);
            Vector2Int nextIndex = GridIndex + nextDirection.ToVector();
            if (!_map.CheckPlatform(nextIndex))
                return;

            Platform platform = _map.GetPlatform(nextIndex);
            if (platform.IsCanMove)
            {
                GridIndex = nextIndex;
                _moveUnit.StartMove(platform.transform.position);
                platform.Event();
            }
        }

        private DirectionType GetDirection()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                return DirectionType.Up;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                return DirectionType.Down;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                return DirectionType.Left;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                return DirectionType.Right;

            return DirectionType.None;
        }

        private void OnUpdateGridMap(IGridMap map)
        {
            _moveUnit.Clear();
            _map = map;
            GridIndex = _map.FindPlatform(PlatformType.PlayerSpawn).GridIndex;
        }
    }
}