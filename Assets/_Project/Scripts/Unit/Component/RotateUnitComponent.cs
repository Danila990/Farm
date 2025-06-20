using UnityEngine;
using System.Threading.Tasks;

namespace MyCode
{
    public class RotateUnitComponent : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 720f;

        public void Setup(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
        }

        public async Task RotateAsync(DirectionType direction)
        {
            if (direction == DirectionType.None)
                return;

            Vector3 targetEuler = direction switch
            {
                DirectionType.Up => new Vector3(0, 0, 0),
                DirectionType.Down => new Vector3(0, 180, 0),
                DirectionType.Left => new Vector3(0, -90, 0),
                DirectionType.Right => new Vector3(0, 90, 0),
                _ => transform.eulerAngles
            };

            Quaternion targetRotation = Quaternion.Euler(targetEuler);

            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                await Task.Yield();
            }

            transform.rotation = targetRotation;
        }
    }
}