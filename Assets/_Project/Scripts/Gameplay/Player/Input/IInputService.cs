using System;

namespace ProjectCode
{
    public interface IInputService
    {
        public DirectionType GetDirection();
        public void Activate();
        public void Deactivate();
    }
}