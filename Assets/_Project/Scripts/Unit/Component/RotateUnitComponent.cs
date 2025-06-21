using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyCode
{
    public class RotateUnitComponent : MonoBehaviour
    {
        [SerializeField] private float _rotateDuraction = 0.2f;

        public void StartRotate(DirectionType typeDirection, bool isFast = false)
        {
            float targetY = typeDirection switch
            {
                DirectionType.Up => 0,
                DirectionType.Down => 180,
                DirectionType.Left => -90,
                DirectionType.Right => 90,
                _ => 0
            };

            Rotation(targetY, isFast);
        }

        private void Rotation(float y, bool isFast)
        {
            if (!isFast)
                transform.DORotate(new Vector3(0, y, 0), _rotateDuraction);
            else
                transform.localRotation = Quaternion.Euler(0, y, 0);
        }
    }
}