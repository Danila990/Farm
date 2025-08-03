using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


namespace ProjectCode
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 360f;
        [SerializeField] private float _jumpHeight = 1f;
        [SerializeField] private float _jumpDuration = 0.5f;
        [SerializeField] private float _offsetPosY;

        private PlayerAnimator _animator;
        private RotateComponent _rotateComponent;
        private JumpComponent _jumpComponent;

        public bool IsMoved => _jumpComponent.IsJumping;
        public bool IsRotated => _rotateComponent.IsRotated;
        public bool IsActive => IsMoved || IsRotated;

        public void Initialize()
        {
            _rotateComponent = new RotateComponent(transform, _rotationSpeed);
            _jumpComponent = new JumpComponent(transform, _jumpHeight, _jumpDuration, _offsetPosY);
            _animator = GetComponent<PlayerAnimator>();
        }

        public async Task MoveAsync(Vector3 targetPos, CancellationToken token = default)
        {
            if (IsMoved) return;

            _animator.StartJumpAnimation();
            await _jumpComponent.JumpAsync(targetPos, token);
            _animator.EndJumpAnimation();
        }

        public async Task RotateAsync(DirectionType rotateDirection, CancellationToken token = default)
        {
            await _rotateComponent.RotateAsync(rotateDirection, token);
        }
    }
}