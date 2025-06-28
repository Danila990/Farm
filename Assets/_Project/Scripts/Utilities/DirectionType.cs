using System.Collections.Generic;
using UnityEngine;

namespace MyCode
{
    public enum DirectionType
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionTypeExtension
    {
        private static readonly Dictionary<DirectionType, Vector2Int> DirectionVectors = new Dictionary<DirectionType, Vector2Int>()
    {
                {DirectionType.Up, new Vector2Int(0, 1)},
                {DirectionType.Down, new Vector2Int(0, -1)},
                {DirectionType.Left, new Vector2Int(-1, 0)},
                {DirectionType.Right, new Vector2Int(1, 0)},
    };

        public static Vector2Int ToVector(this DirectionType directionType)
        {
            if (DirectionVectors.TryGetValue(directionType, out Vector2Int index))
                return index;

            throw new System.Exception("Direction error");
        }

        public static DirectionType ToDirection(this Vector2Int directionType)
        {
            foreach (var direction in DirectionVectors)
            {
                if (direction.Value == directionType)
                    return direction.Key;
            }

            throw new System.Exception("Vector error");
        }
    }
}