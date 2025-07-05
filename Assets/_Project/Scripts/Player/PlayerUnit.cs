using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace MyCode
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private MoveUnit _moveUnit;
        [SerializeField] private RotateUnit _rotateUnit;
        [SerializeField] private PlayerInputUnit _inputUnit;
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
                DirectionType nextDirection = _inputUnit.GetDirection();
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
                if (platform.CanMove)
                {
                    GridIndex = nextIndex;
                    await _moveUnit.JumpToAsync(platform.transform.position, _moveCts.Token);
                    platform.Event();
                }
                else
                    await Task.Yield();
            }
        }
    }
}