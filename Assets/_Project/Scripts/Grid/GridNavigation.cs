using System;
using UnityEngine;
using VContainer;

namespace MyCode
{
    public class GridNavigation
    {
        [Inject] private GridController _gridController;

        private GridMap _gridMap;
        private Vector2Int _sizeGrid;

        public void Initialize()
        {
            _gridMap = _gridController.GetCurrentMap();
            _sizeGrid = _gridMap.GetSizeGrid();
            _gridController.OnChangeMap += UpdateMap;
        }

        public bool CheckPlatform(Vector2Int platformIndex)
        {
            if (platformIndex.x < 0 || platformIndex.y < 0 ||
                platformIndex.x >= _sizeGrid.x || platformIndex.y >= _sizeGrid.y)
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

        private void UpdateMap(GridMap map)
        {
            _gridMap = map;
            _sizeGrid = _gridMap.GetSizeGrid();
        }
    }
}