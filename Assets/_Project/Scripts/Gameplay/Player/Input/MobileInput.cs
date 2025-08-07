using System;
using UnityEngine;

namespace ProjectCode
{
    public class MobileInput : MonoBehaviour, IInputService
    {
        public event Action<DirectionType> OnInputDirection;

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }
    }
}