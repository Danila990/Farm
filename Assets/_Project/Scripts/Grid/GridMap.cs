using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace MyCode
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] private GridLine[] _grid;

        public void SetupMap(GridLine[] grid)
        {
            _grid = grid;
        }

        public Vector2Int GetSizeGrid()
        {
            return new Vector2Int(_grid.Length, _grid[0].platforms.Length);
        }

        public Platform GetPlatform(Vector2Int index)
        {
            int clampX = math.clamp(index.x, 0, _grid.Length);
            int clampY = math.clamp(index.y, 0, _grid[0].platforms.Length);
            return _grid[clampX].platforms[clampY];
        }

        public Platform FindPlatform(PlatformType platform)
        {
            return _grid
                .SelectMany(line => line.platforms)
                .Where(p => p != null && p.platformType == platform)
                .FirstOrDefault();
        }

        public Platform[] FindPlatforms(PlatformType platform)
        {
            return _grid
                .SelectMany(line => line.platforms)
                .Where(p => p != null && p.platformType == platform)
                .ToArray();
        }
    }

    [Serializable]
    public struct GridLine
    {
        public Platform[] platforms;
    }
}