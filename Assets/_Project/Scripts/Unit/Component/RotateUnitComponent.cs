using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

namespace MyCode
{
    public class RotateUnitComponent : MonoBehaviour
    {
        private readonly Dictionary<DirectionType, int> _directions = new()
        {
                {DirectionType.Up, 0 },
                {DirectionType.Down, 180},
                {DirectionType.Left, -90},
                {DirectionType.Right, 90},
        };

        [SerializeField] private float _rotateDuraction = 0.2f;

        [field: SerializeField] public DirectionType currentDirection { get; private set; }

        public void StartRotate(DirectionType typeDirection, bool isFast = false)
        {
            if (_directions.ContainsKey(typeDirection))
            {
                currentDirection = typeDirection;
                Rotation(_directions[typeDirection], isFast);
                return;
            }

            throw new NullReferenceException($"null currentDirection type");
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