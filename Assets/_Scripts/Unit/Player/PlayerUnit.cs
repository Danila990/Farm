using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Code
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private UnitRotate _modelRotater;
        [SerializeField] private PlayerInputUnit _playerInput;
        [SerializeField] private PlayerArrowRotate _arrowRotater;

        private CancellationTokenSource _cts;
        private DirectionType _nextDirection;
        private DirectionType _currentDirection;
        private DirectionType _updateDirection;

        private void Update()
        {
            _updateDirection = _playerInput.GetDirection();
            if(_updateDirection != DirectionType.None)
            {
                _nextDirection = _updateDirection;
                _arrowRotater.ActivateArrow(_nextDirection);
            }
        }

        private void CheckDirection()
        {
            if (_nextDirection == DirectionType.None)
            {
                if (!_playerMover.CheckNext(_currentDirection))
                    _currentDirection = DirectionType.None;
            }
            else
                _currentDirection = _nextDirection;
        }

        public async void StartMoved()
        {
            _cts = new CancellationTokenSource();
            while (true)
            {
                _cts?.Cancel();

                CheckDirection();
                if (_currentDirection == DirectionType.None)
                {
                    _arrowRotater.DeactivateArrow();
                    await Task.Yield();
                    continue;
                }

                await _modelRotater.RotateToAsync(_currentDirection, _cts.Token);
                await _playerMover.Moved(_currentDirection, _cts.Token);
            }
        }
    }
}