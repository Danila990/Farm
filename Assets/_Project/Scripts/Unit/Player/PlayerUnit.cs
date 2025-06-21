using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace MyCode
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private RotateUnitComponent _rotateComponent;
        [SerializeField] private MoveUnitComponent _moveComponent;

        private IGridMap _gridMap;
        private Vector2Int _gridIndex;
        private bool _isMove = false;

        public void SetupUnit(IGridMap gridMap, Vector2Int gridIndex)
        {
            StopMove();
            _gridMap = gridMap;
            _gridIndex = gridIndex;
        }

        public void StopMove()
        {
            _isMove = false;
        }

        public void StartMove()
        {
            _isMove = true;
        }

        private void Update()
        {
            if (!_isMove || _moveComponent.IsMoved)
                return;

            DirectionType nextDirection = GetDirection();
            if(nextDirection == DirectionType.None)
                return;

            _rotateComponent.StartRotate(nextDirection);
            Vector2Int nextIndex = _gridIndex + nextDirection.ToVector();
            if (_gridMap.TryGetPlatform(nextIndex, out Platform platform))
            {
                if (platform.IsCanMove)
                {
                    _gridIndex = nextIndex;
                    _moveComponent.StartMove(platform.transform.position);
                    platform.Event();
                }
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
    }
}