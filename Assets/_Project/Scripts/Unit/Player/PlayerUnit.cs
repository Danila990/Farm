using UnityEngine;

namespace MyCode
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private RotateUnitComponent _rotateComponent;
        [SerializeField] private MoveUnitComponent _moveComponent;
        [SerializeField] private GridNavigation _navigationComponent;

        [SerializeField] private PcInputService _qcInputService;

        private Vector2Int _gridIndex;
        private DirectionType _nextDirection;

        public void Setup(Vector2Int gridIndex)
        {
            //_navigationComponent.Setup();
            _gridIndex = gridIndex;
            _moveComponent.OnMoveComplete += OnMoveCompleted;
            _qcInputService.OnMoveInput += OnMoveInput;
        }

        private void OnMoveInput(DirectionType directionType)
        {
            _nextDirection = directionType;
            if (!_moveComponent.IsMoved)
                OnMoveCompleted();
        }

        private void OnMoveCompleted()
        {
            Move(_nextDirection);
            if(_moveComponent.IsMoved)
                return;

            _nextDirection = _rotateComponent.currentDirection;
            Move(_rotateComponent.currentDirection);
        }

        private void Move(DirectionType nextMoveDirection)
        {
            Vector2Int nextIndex = _gridIndex + nextMoveDirection.ToVector();
            if (!_navigationComponent.CheckPlatform(nextIndex))
                return;

            Platform platform = _navigationComponent.GetPlatform(nextIndex);
            if (!platform.IsCanMove)
                return;

            _gridIndex = nextIndex;
            _rotateComponent.StartRotate(nextMoveDirection);
            _moveComponent.StartMove(platform.transform.position);
        }

        private void OnDestroy()
        {
            _moveComponent.OnMoveComplete -= OnMoveCompleted;
            _qcInputService.OnMoveInput -= OnMoveInput;
        }
    }
}