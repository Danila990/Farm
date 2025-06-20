using System;
using UnityEngine;

namespace MyCode
{
    public interface IInputService
    {
        public event Action<DirectionType> OnMoveInput;
        public void Initialize();
        public void Activate();
        public void Deactivate();
    }
}