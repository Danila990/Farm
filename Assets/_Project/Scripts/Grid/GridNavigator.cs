using System.Collections.Generic;
using System;
using UnityEngine;

namespace MyCode
{
    public class GridNavigator : IDisposable
    {
        private GridController _gridController;
        private GridMap _gridMap;
        private Vector2Int _sizeGrid;

        public void Construct(GridController gridController)
        {
            _gridController = gridController;
            _gridController.ChangeMap += UpdateMap;
        }

        public bool CheckPlatform(Vector2Int platformIndex)
        {
            if (platformIndex.x < 0 || platformIndex.y < 0 ||
                platformIndex.x > _sizeGrid.x || platformIndex.y > _sizeGrid.y)
                return false;

            return true;
        }

        public bool TryGetPlatform(Vector2Int platformIndex, out Platform platform)
        {
            if (CheckPlatform(platformIndex))
            {
                platform = _gridMap.GetPlatform(platformIndex);
                return true;
            }

            platform = null;
            return false;
        }

        public Platform GetPlatform(Vector2Int platformIndex)
        {
            if (!CheckPlatform(platformIndex))
                throw new ArgumentException();

            return _gridMap.GetPlatform(platformIndex);
        }

        public void Dispose()
        {
            _gridController.ChangeMap -= UpdateMap;
        }

        private void UpdateMap(GridMap map)
        {
            _gridMap = map;
            _sizeGrid = _gridMap.GetSizeGrid();
        }
    }
}