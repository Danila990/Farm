using UnityEngine;

namespace ProjectCode
{
    public class PcInput : MonoBehaviour, IInputService
    {
        private bool _isActive = false;
        private DirectionType _currentDirection = DirectionType.None;

        public void Activate()
        {
            _isActive = true;
            _currentDirection = DirectionType.None;
        }

        public void Deactivate()
        {
            _isActive = false;
            _currentDirection = DirectionType.None;
        }

        public DirectionType GetDirection()
        {
            return _currentDirection;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                _currentDirection = DirectionType.Up;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                _currentDirection = DirectionType.Down;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                _currentDirection = DirectionType.Left;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                _currentDirection = DirectionType.Right;
        }
    }
}