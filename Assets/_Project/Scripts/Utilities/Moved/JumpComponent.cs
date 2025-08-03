using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class JumpComponent
    {
        private readonly float _jumpHeight = 1f;
        private readonly float _jumpDuration = 0.5f;
        private readonly float _offsetPosY;
        private Transform _target;

        public bool IsJumping { get; private set; } = false;

        public JumpComponent(Transform target,float jumpHeight, float jumpDuration, float offsetPosY)
        {
            _target = target;
            _jumpHeight = jumpHeight;
            _jumpDuration = jumpDuration;
            _offsetPosY = offsetPosY;
        }

        public async Task JumpAsync(Vector3 targetPosition, CancellationToken token = default)
        {
            if (IsJumping) return;

            IsJumping = true;
            targetPosition.y += _offsetPosY;
            Vector3 startPosition = _target.position;
            float elapsed = 0f;
            while (elapsed < _jumpDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _jumpDuration);

                Vector3 horizontalPos = Vector3.Lerp(startPosition, targetPosition, t);
                float heightOffset = 4 * _jumpHeight * t * (1 - t);
                _target.position = new Vector3(horizontalPos.x, horizontalPos.y + heightOffset, horizontalPos.z);

                await Task.Yield();
            }

            _target.position = targetPosition;
            IsJumping = false;
        }

        public async void Moved(Vector3 targetPosition, CancellationToken token = default)
        {
            await JumpAsync(targetPosition, token);
        }
    }
}