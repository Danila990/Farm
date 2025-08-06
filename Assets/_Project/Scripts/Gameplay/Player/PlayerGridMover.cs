using Project1;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class PlayerGridMover : MonoBehaviour
    {
        private PlayerAnimator _animator;
        private RotateComponent _rotateComponent;
        private JumpComponent _jumpComponent;
        private CancellationTokenSource _cts;

        private IGridMap _gridMap;
        private IInputService _inputService;
        private Vector2Int _gridIndex;

        public bool IsMoved => _jumpComponent.IsJumping;
        public bool IsRotated => _rotateComponent.IsRotated;
        public bool IsActive => IsMoved || IsRotated;

        public void Init(PlayerInfo playerInfo)
        {
            _cts = new CancellationTokenSource();
            _rotateComponent = new RotateComponent(transform, playerInfo.RotateSpeeed);
            _jumpComponent = new JumpComponent(transform, playerInfo.JumpHeight, playerInfo.JumpDuration, playerInfo.OffsetPosY);
            _animator = GetComponent<PlayerAnimator>();
            _inputService = ServiceLocator.Get<IInputService>();
        }

        public void Startable()
        {
            _gridMap = ServiceLocator.Get<IGridMap>();
            _gridIndex = _gridMap.FindPlatform(PlatformType.Player).GridIndex;
        }

        private async void Update()
        {
            if (IsActive) return;

            DirectionType nextDirection = _inputService.GetDirection();
            _cts?.Cancel();
            await Move(nextDirection, _cts.Token);
        }

        private async Task Move(DirectionType nextDirection, CancellationToken token = default)
        {
            if (IsActive || nextDirection == DirectionType.None) return;

            Vector2Int nextIndex = _gridIndex + nextDirection.ToVector();
            await _rotateComponent.RotateAsync(nextDirection, token);
            await MovedToPlatform(nextIndex);
        }

        private async Task MovedToPlatform(Vector2Int nextIndex, CancellationToken token = default)
        {
            if (_gridMap.TryGetPlatform(nextIndex, out Platform platform))
            {
                if (platform.CanMove)
                {
                    _animator.StartJumpAnimation();
                    await _jumpComponent.JumpAsync(platform.transform.position, token);
                    _animator.EndJumpAnimation();
                    _gridIndex = nextIndex;
                    platform.Event();
                }
            }
        }
    }
}