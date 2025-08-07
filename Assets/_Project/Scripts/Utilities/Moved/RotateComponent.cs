using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class RotateComponent
    {
        private readonly float _rotationSpeed = 360f;
        private readonly Transform _target;

        public bool IsRotated {  get; private set; } = false;

        public RotateComponent(Transform target, float rotationSpeed)
        {
            _target = target;
            _rotationSpeed = rotationSpeed;
        }

        public async Task RotateAsync(DirectionType typeDirection, CancellationToken token = default)
        {
            if (IsRotated || typeDirection == DirectionType.None) return;

            IsRotated = true;
            Quaternion targetRotation = typeDirection.ToQuaternionY();
            while (Quaternion.Angle(_target.rotation, targetRotation) > 0.1f)
            {
                _target.rotation = Quaternion.RotateTowards(_target.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                await Task.Yield();
            }

            _target.rotation = targetRotation;
            IsRotated = false; 
        }

        public async void Rotate(DirectionType typeDirection, CancellationToken token = default)
        {
            await RotateAsync(typeDirection, token);
        }
    }
}