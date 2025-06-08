using System.Collections.Generic;
using System;
using UnityEngine;

namespace MyCode
{
    public class GridNavigator : MonoBehaviour
    {
        private Platform[,] _platforms;
        private int _lengthX;
        private int _lengthY;

        public void Init(Platform[,] platforms)
        {
            _platforms = platforms;
            _lengthX = _platforms.Length;
            _lengthY = _platforms.GetLength(1);
        }

        public bool CheckPlatform(Vector2Int platformIndex)
        {
            if (platformIndex.x < 0 || platformIndex.y < 0 ||
                platformIndex.x > _lengthX || platformIndex.y > _lengthY)
                return false;

            return true;
        }

        public bool TryGetPlatform(Vector2Int platformIndex, out Platform platform)
        {
            if (CheckPlatform(platformIndex))
            {
                platform = _platforms[platformIndex.x, platformIndex.y];
                return true;
            }

            platform = null;
            return false;
        }

        public Platform GetPlatform(Vector2Int platformIndex)
        {
            if (!CheckPlatform(platformIndex))
                throw new ArgumentException();

            return _platforms[platformIndex.x, platformIndex.y];
        }
    }
}