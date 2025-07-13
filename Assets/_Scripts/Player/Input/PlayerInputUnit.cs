using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public class PlayerInputUnit : MonoBehaviour
    {
        [SerializeField] private bool _isPc = true; 

        public DirectionType GetDirection()
        {
            if (!_isPc)
                return MobileInput();
            else
                return PcInput();
        }

        private DirectionType MobileInput()
        {
            throw new NotImplementedException();
        }

        private DirectionType PcInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                return DirectionType.Up;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                return DirectionType.Down;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                return DirectionType.Left;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                return DirectionType.Right;

            return DirectionType.None;
        }
    }
}