using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace MyCode
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private MoveUnit _moveUnit;
        [SerializeField] private RotateUnit _rotateUnit;
        public Vector2Int GridIndex { get; private set; }

        private IGridMap _map;
        private CancellationTokenSource _moveCts;

        public void Startable(IGridMap map, Vector2Int gridIndex)
        {
            _map = map;
            GridIndex = gridIndex;
            Moved();
        }

        private async void Moved()
        {
            while (true)
            {
                DirectionType nextDirection = GetDirection();
                if (nextDirection == DirectionType.None)
                {
                    await Task.Yield();
                    continue;
                }

                _moveCts = new CancellationTokenSource();
                _moveCts?.Cancel();

                _rotateUnit.StartRotate(nextDirection);
                Vector2Int nextIndex = GridIndex + nextDirection.ToVector();
                if (!_map.CheckPlatform(nextIndex))
                {
                    await Task.Yield();
                    continue;
                }

                Platform platform = _map.GetPlatform(nextIndex);
                if (platform.IsCanMove)
                {
                    GridIndex = nextIndex;
                    await _moveUnit.MoveToAsync(platform.transform.position, _moveCts.Token);
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