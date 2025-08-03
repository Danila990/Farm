using UnityEngine;

namespace ProjectCode
{
    public interface IGridMap
    {
        public Platform GetPlatform(Vector2Int index);
        public bool CheckPlatform(Vector2Int index);
        public Platform FindPlatform(PlatformType platform);
        public Platform[] FindPlatforms(PlatformType platform);
        public bool TryGetPlatform(Vector2Int index, out Platform result);
    }
}