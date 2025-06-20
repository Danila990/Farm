using System;
using UnityEngine;

namespace MyCode
{
    public class MobileInputService : MonoBehaviour, IInputService
    {
        private bool _isActive = false;

        public event Action<DirectionType> OnMoveInput;

        public void Initialize()
        {
            
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        public void Update()
        {
            if (!_isActive)
                return;

            if (Input.GetKeyDown(KeyCode.D))
            {
                OnMoveInput.Invoke(DirectionType.Right);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                OnMoveInput.Invoke(DirectionType.Left);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                OnMoveInput.Invoke(DirectionType.Up);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                OnMoveInput.Invoke(DirectionType.Down);
            }
        }
    }
}