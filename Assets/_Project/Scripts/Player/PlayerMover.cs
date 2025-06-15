using System.Collections;
using UnityEngine;

namespace MyCode
{
    public class PlayerMover : MonoBehaviour
    {
        private float _moveSpeed;
        private float _rotateSpeed;

        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotateSpeed;

        public void Setup(float moveSpeed, float rotateSpeed)
        {
            _moveSpeed = moveSpeed;
            _rotateSpeed = rotateSpeed;
        }
    }
}