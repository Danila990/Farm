using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight = 1f;
        [SerializeField] private float _jumpDuration = 0.5f;
        [SerializeField] private float _rotationSpeed = 360f;
        [SerializeField] private float _offsetPosY;
        [SerializeField] private Transform _rotateModel;

        private PlayerAnimator _animator;
        private JumpComponent _jumpComponent;
        private RotateComponent _rotateComponent;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public bool IsRotated => _rotateComponent.IsRotated;
        public bool IsJumping => _jumpComponent.IsJumping;
        public bool IsMoved => _jumpComponent.IsJumping || _rotateComponent.IsRotated;


        public void Init()
        {
            _animator = GetComponent<PlayerAnimator>();
            _jumpComponent = new JumpComponent(transform, _jumpHeight, _jumpDuration, _offsetPosY);
            _rotateComponent = new RotateComponent(_rotateModel.transform, _rotationSpeed);
        }

        public async void Moved(Vector3 targetPosition , DirectionType typeDirection)
        {
            await RotateAsync(typeDirection);
            await JumpAsync(targetPosition);
        }

        public async Task JumpAsync(Vector3 targetPosition)
        {
            _animator.StartJumpAnimation();
            await _jumpComponent.JumpAsync(targetPosition, _cts.Token);
            _animator.EndJumpAnimation();
        }

        public async Task RotateAsync(DirectionType typeDirection)
        {
            await _rotateComponent.RotateAsync(typeDirection, _cts.Token);
        }
    }
}