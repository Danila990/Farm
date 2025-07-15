using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using VContainer;

namespace Code
{
    public class PlayerMover : UnitMover
    {
        [SerializeField] private float _awaitDelay = 0.1f;
        [SerializeField] private PlayerAnimatorUnity _animator;

        [Inject] private IGridService _gridService;

        private IGridMap _map;
        private Vector2Int _gridIndex;

        private void Awake()
        {
            _map = _gridService.GetGridMap();
            _gridIndex = _map.FindPlatform(PlatformType.PlayerSpawn).GridIndex;
        }

        public bool CheckNext(DirectionType nextDirection)
        {
            Vector2Int nextIndex = _gridIndex + nextDirection.ToVector();
            return _map.CheckPlatform(nextIndex);
        }

        public async Task Moved(DirectionType nextDirection, CancellationToken token = default)
        {
            Vector2Int nextIndex = _gridIndex + nextDirection.ToVector();
            if (!_map.CheckPlatform(nextIndex))
            {
                await Task.Yield();
                return;
            }

            Platform platform = _map.GetPlatform(nextIndex);
            if (platform.CanMove)
            {
                _animator.StartJumpAnimation();
                _gridIndex = nextIndex;
                await JumpToAsync(platform.transform.position, token);
                platform.Event();
                _animator.EndJumpAnimation();
            }
            else
                await Task.Yield();

            await Task.Delay((int)(_awaitDelay * 1000));
        }
    }
}