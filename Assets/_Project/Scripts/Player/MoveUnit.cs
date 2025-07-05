using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace MyCode
{
    public class MoveUnit : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private Vector3 _moveOffset;

        [Tooltip("Высота прыжка")]
        public float jumpHeight = 2f;

        [Tooltip("Скорость движения")]
        public float jumpDuration = 1f;

        public async Task JumpToAsync(Vector3 targetPosition, CancellationToken token = default)
        {
            targetPosition += _moveOffset;
            Vector3 startPosition = transform.position;
            float elapsed = 0f;

            while (elapsed < jumpDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / jumpDuration);

                Vector3 horizontalPos = Vector3.Lerp(startPosition, targetPosition, t);
                float heightOffset = 4 * jumpHeight * t * (1 - t);
                transform.position = new Vector3(horizontalPos.x, horizontalPos.y + heightOffset, horizontalPos.z);

                await Task.Yield();
            }

            transform.position = targetPosition;
        }
    }
}