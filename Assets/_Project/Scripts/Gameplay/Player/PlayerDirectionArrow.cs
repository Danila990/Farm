using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class PlayerDirectionArrow : MonoBehaviour
    {
        [SerializeField] private GameObject _arrow;
        [SerializeField] private float _rotateSpeeed = 600f;

        private RotateComponent _rotate;
        private CancellationTokenSource _cts;
        private DirectionType _currentDirection;
        private DirectionType _nextDirection;

        private void OnEnable() => ServiceLocator.Get<IInputService>().OnInputDirection += OnUpdateDirection;
        private void OnDestroy() => ServiceLocator.Get<IInputService>().OnInputDirection -= OnUpdateDirection;

        public void Init()
        {
            _rotate = new RotateComponent(_arrow.transform, _rotateSpeeed);
            _cts = new CancellationTokenSource();
            RotateWhile();
        }

        private async void RotateWhile()
        {
            if (_rotate.IsRotated || _nextDirection == DirectionType.None || _nextDirection == _currentDirection) return;

            _currentDirection = _nextDirection;
            _cts?.Cancel();
            _rotate.Rotate(_currentDirection, _cts.Token);
            RotateWhile();
        }

        private void OnUpdateDirection(DirectionType directionType)
        {
            _nextDirection = directionType;
            RotateWhile();
        }
    }
}