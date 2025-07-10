using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace MyCode
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MoveUnit _moveUnit;
        [SerializeField] private RotateUnit _rotateUnit;
        [SerializeField] private PlayerInputUnit _inputUnit;
        [SerializeField] private PlayerAnimatorUnity _animatorUnit;

        public Vector2Int GridIndex { get; private set; }

        private IGridMap _map;
        private int _awaitDuration;
        private CancellationTokenSource _moveCts;

        public void Setup(IGridMap map, Vector2Int gridIndex, PlayerSettings playerSettings)
        {
            _map = map;
            GridIndex = gridIndex;
            _moveUnit.Setup(playerSettings.JumpHeight, playerSettings.JumpDuration, playerSettings.OffsetY);
            _rotateUnit.Setup(playerSettings.RotateDuration);
            _awaitDuration = (int)(playerSettings.AwaitDuration * 1000);
        }

        public async void Moved()
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
                    _animatorUnit.StartJumpAnimation();
                    GridIndex = nextIndex;
                    await _moveUnit.JumpToAsync(platform.transform.position, _moveCts.Token);
                    platform.Event();
                    _animatorUnit.EndJumpAnimation();
                    await Task.Delay(_awaitDuration);
                }
                else
                    await Task.Yield();
            }
        }
    }
}