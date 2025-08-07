using System;
using UnityEngine;

namespace ProjectCode
{
    public class PcInput : MonoBehaviour, IInputService
    {
        public event Action<DirectionType> OnInputDirection;

        private bool _isActive = false;

        public void Activate()
        {
            _isActive = true;
            OnInputDirection?.Invoke(DirectionType.None);
        }

        public void Deactivate()
        {
            _isActive = false;
            OnInputDirection?.Invoke(DirectionType.None);
        }

        private void Update()
        {
            if (!_isActive)
                return;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                OnInputDirection?.Invoke(DirectionType.Up);
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                OnInputDirection?.Invoke(DirectionType.Down);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                OnInputDirection?.Invoke(DirectionType.Left);
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                OnInputDirection?.Invoke(DirectionType.Right);
        }
    }
}