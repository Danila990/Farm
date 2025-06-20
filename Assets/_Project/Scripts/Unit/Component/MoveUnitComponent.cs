using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MyCode
{
    public class MoveUnitComponent : MonoBehaviour
    {

        [SerializeField] private float _moveSpeed = 5f;

        public void Setup(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public async Task MoveAsync(Vector3 targetPosition, CancellationToken token = default)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
                await Task.Yield();
            }

            transform.position = targetPosition;
        }
    }
}