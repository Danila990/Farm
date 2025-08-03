using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace ProjectCode
{
    [RequireComponent (typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private IInputService _input;
        private IGridMap _gridMap;
        private CancellationTokenSource _cts;

        public Vector2Int PlayerIndex { get; private set; }

        public void Initialize()
        {
            _cts = new CancellationTokenSource();
            _input = ServiceLocator.Get<IInputService>();
            _gridMap = ServiceLocator.Get<IGridMap>();
            PlayerIndex = _gridMap.FindPlatform(PlatformType.Player).GridIndex;
            _playerMover = GetComponent<PlayerMover>();
            _playerMover.Initialize();
            MoveTick();
        }

        private async void MoveTick()
        {
            while (true)
            {
                DirectionType nextDirection = _input.GetDirection();
                if (_playerMover.IsActive || nextDirection == DirectionType.None)
                {
                    await Task.Yield();
                    continue;
                }

                _cts?.Cancel();
                Vector2Int nextIndex = PlayerIndex + nextDirection.ToVector();
                await _playerMover.RotateAsync(nextDirection, _cts.Token);
                await MovedToPlatform(nextIndex);
                await Task.Yield();
            }
        }

        private async Task MovedToPlatform(Vector2Int nextIndex)
        {
            if (_gridMap.TryGetPlatform(nextIndex, out Platform platform))
            {
                if (platform.CanMove)
                {
                    await _playerMover.MoveAsync(platform.transform.position, _cts.Token);
                    PlayerIndex = nextIndex;
                    platform.Event();
                }
            }
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
        }
    }
}