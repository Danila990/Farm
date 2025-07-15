using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Code
{
    public class UnitRotate : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 360f;

        public async Task RotateToAsync(DirectionType typeDirection, CancellationToken token = default)
        {
            if (typeDirection == DirectionType.None)
                return;

            Quaternion targetRotation = DirectionToQuaternion(typeDirection);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                await Task.Yield();
            }

            transform.rotation = targetRotation;
        }

        private Quaternion DirectionToQuaternion(DirectionType typeDirection)
        {
            return typeDirection switch
            {
                DirectionType.Up => Quaternion.Euler(0, 0, 0),
                DirectionType.Down => Quaternion.Euler(0, 180, 0),
                DirectionType.Left => Quaternion.Euler(0, 270, 0),
                DirectionType.Right => Quaternion.Euler(0, 90, 0),
                _ => transform.rotation
            };
        }
    }
}