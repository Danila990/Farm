using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Code
{
    public class UnitMover : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight = 1f;
        [SerializeField] private float _jumpDuration = 0.5f;
        [SerializeField] private float _offsetPosY;

        public async Task JumpToAsync(Vector3 targetPosition, CancellationToken token = default)
        {
            targetPosition.y += _offsetPosY;
            Vector3 startPosition = transform.position;
            float elapsed = 0f;

            while (elapsed < _jumpDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _jumpDuration);

                Vector3 horizontalPos = Vector3.Lerp(startPosition, targetPosition, t);
                float heightOffset = 4 * _jumpHeight * t * (1 - t);
                transform.position = new Vector3(horizontalPos.x, horizontalPos.y + heightOffset, horizontalPos.z);

                await Task.Yield();
            }

            transform.position = targetPosition;
        }
    }
}