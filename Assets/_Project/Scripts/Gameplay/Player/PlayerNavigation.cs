using UnityEngine;

namespace ProjectCode
{
    public class PlayerNavigation : MonoBehaviour
    {
        [SerializeField] private PlayerMover _playerMover;

        private IGridMap _gridMap;
        private Vector2Int _gridIndex;
        private DirectionType _currentDirection;

        private void OnEnable() => ServiceLocator.Get<IInputService>().OnInputDirection += OnUpdateDirection;

        private void OnDestroy() => ServiceLocator.Get<IInputService>().OnInputDirection -= OnUpdateDirection;

        public void Setup()
        {
            _gridMap = ServiceLocator.Get<IGridMap>();
            _gridIndex = _gridMap.FindPlatform(PlatformType.Player).GridIndex;
            MoveWhile();
        }

        private async void MoveWhile()
        {
            if (_playerMover.IsMoved || _currentDirection == DirectionType.None) return;

            Vector2Int nextIndex = _gridIndex + _currentDirection.ToVector();
            await _playerMover.RotateAsync(_currentDirection);

            if(!_gridMap.CheckPlatform(nextIndex)) return;

            Platform platform = _gridMap.GetPlatform(nextIndex);
            if (platform.CanMove)
            {
                await _playerMover.JumpAsync(platform.transform.position);
                _gridIndex = nextIndex;
                platform.Event();
                MoveWhile();
            }
        }

        private void OnUpdateDirection(DirectionType directionType)
        {
            _currentDirection = directionType;
            MoveWhile();
        }
    }
}