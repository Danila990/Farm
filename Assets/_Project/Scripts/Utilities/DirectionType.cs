using System.Collections.Generic;
using UnityEngine;

namespace ProjectCode
{
    public enum DirectionType
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }

    public static class DirectionTypeExtension
    {
        private static readonly Dictionary<DirectionType, Vector2Int> _directionVectors = new Dictionary<DirectionType, Vector2Int>()
        {
                {DirectionType.None, Vector2Int.zero},
                {DirectionType.Up, Vector2Int.up},
                {DirectionType.Down, Vector2Int.down},
                {DirectionType.Left, Vector2Int.left},
                {DirectionType.Right, Vector2Int.right},
        };

        public static Quaternion ToQuaternionY(this DirectionType directionType)
        {
            return directionType switch
            {
                DirectionType.Up => Quaternion.Euler(0, 0, 0),
                DirectionType.Down => Quaternion.Euler(0, 180, 0),
                DirectionType.Left => Quaternion.Euler(0, 270, 0),
                DirectionType.Right => Quaternion.Euler(0, 90, 0),
                _ => throw new System.Exception("Direction type To Quaternion mistake"),
            };
        }

        public static Vector2Int ToVector(this DirectionType directionType)
        {
            if (_directionVectors.TryGetValue(directionType, out Vector2Int index))
                return index;

            throw new System.Exception("Direction type To Vector mistake");
        }

        public static DirectionType ToDirection(this Vector2Int directionType)
        {
            foreach (var direction in _directionVectors)
            {
                if (direction.Value == directionType)
                    return direction.Key;
            }

            throw new System.Exception("Vector To Direction type mistake");
        }
    }
}