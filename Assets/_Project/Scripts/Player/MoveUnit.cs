using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MyCode
{
    public class MoveUnit : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private Vector3 _moveOffset;

        public async Task MoveToAsync(Vector3 targetPosition, CancellationToken token = default)
        {
            targetPosition += _moveOffset;
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
                await Task.Yield();
            }

            transform.position = targetPosition;
        }
    }
}