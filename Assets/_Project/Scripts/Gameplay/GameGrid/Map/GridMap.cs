using System.Linq;
using UnityEngine;

namespace ProjectCode
{
    public class GridMap : MonoBehaviour, IGridMap
    {
        [SerializeField] private TwoDimensionalArray<Platform> _grid = new TwoDimensionalArray<Platform>();

        public void SetupMap(ArrayLine<Platform>[] values)
        {
            _grid.Set(values);
        }

        public Platform GetPlatform(Vector2Int index)
        {
            return _grid.Get(index.x, index.y);
        }

        public Platform FindPlatform(PlatformType platform)
        {
            return _grid.GetAll()
                .SelectMany(line => line.LineValues)
                .Where(p => p != null && p.PlatformType == platform)
                .FirstOrDefault();
        }

        public T[] FindPlatforms<T>(PlatformType platform) where T : Platform
        {
            return _grid.GetAll()
                .SelectMany(line => line.LineValues)
                .Where(p => p != null && p.PlatformType == platform)
                .ToArray() as T[];
        }

        public bool CheckPlatform(Vector2Int index)
        {
            return _grid.Check(index.x, index.y);
        }

        public bool TryGetPlatform(Vector2Int index, out Platform result)
        {
            if (!CheckPlatform(index))
            {
                result = null;
                return false;
            }

            result = GetPlatform(index);
            return true;
        }
    }

}