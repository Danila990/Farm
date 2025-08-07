using System;

namespace ProjectCode
{
    public interface IInputService
    {
        public event Action<DirectionType> OnInputDirection;
        public void Activate();
        public void Deactivate();
    }
}