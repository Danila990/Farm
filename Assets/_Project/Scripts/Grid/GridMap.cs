using System;
using System.Linq;
using UnityEngine;

namespace MyCode
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] private TwoDimensionalArray<Platform> _grid = new TwoDimensionalArray<Platform>();

        public void SetupMap(ArrayLine<Platform>[] values) => _grid.Set(values);

        public Vector2Int GetSizeGrid() => _grid.GetSize();

        public Platform GetPlatform(Vector2Int index) => _grid.Get(index.x, index.y);

        public Platform FindPlatform(PlatformType platform)
        {
            return _grid.GetAll()
                .SelectMany(line => line.LineValues)
                .Where(p => p != null && p.platformType == platform)
                .FirstOrDefault();
        }

        public Platform[] FindPlatforms(PlatformType platform)
        {
            return _grid.GetAll()
                .SelectMany(line => line.LineValues)
                .Where(p => p != null && p.platformType == platform)
                .ToArray();
        }
    }
}