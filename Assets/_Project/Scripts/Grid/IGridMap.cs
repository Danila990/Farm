using UnityEngine;

namespace MyCode
{
    public interface IGridMap
    {
        public Vector2Int GetSizeGrid();
        public Platform GetPlatform(Vector2Int index);
        public bool CheckPlatform(Vector2Int index);
        public bool TryGetPlatform(Vector2Int index, out Platform platform);
        public Platform FindPlatform(PlatformType platform);
        public Platform[] FindPlatforms(PlatformType platform);
    }
}