using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace MyCode
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private PlayerInputController _playerInput;
        [SerializeField] private RotateUnitComponent _rotateComponent;
        [SerializeField] private MoveUnitComponent _moveComponent;

        [Inject] private GridNavigation _gridNavigation;

        private CancellationTokenSource _moveCts;
        private Vector2Int _gridIndex;

        public void Setup(Vector2Int gridIndex, float moveSpeed, float rotateSpeed)
        {
            _gridIndex = gridIndex;
            _moveComponent.Setup(moveSpeed);
            _rotateComponent.Setup(rotateSpeed);
            StartMove();
        }

        private async void StartMove()
        {
            while (true)
            {
                DirectionType nextDirection = await _playerInput.WaitForDirectionAsync();

                _moveCts?.Cancel();
                _moveCts = new CancellationTokenSource();

                await _rotateComponent.RotateAsync(nextDirection);

                Vector2Int nextIndex = _gridIndex + nextDirection.ToVector();
                if (_gridNavigation.TryGetPlatform(nextIndex, out Platform platform))
                {
                    _gridIndex = nextIndex;
                    await _moveComponent.MoveAsync(platform.transform.position, _moveCts.Token);
                }

                await Task.Yield();
            }
        }
    }
}