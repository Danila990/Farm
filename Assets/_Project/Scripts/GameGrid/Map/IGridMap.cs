using UnityEngine;

namespace ProjectCode
{
    public interface IGridMap
    {
        public Platform GetPlatform(Vector2Int index);
        public bool CheckPlatform(Vector2Int index);
        public Platform FindPlatform(PlatformType platform);
        public T[] FindPlatforms<T>(PlatformType platform) where T : Platform;
        public bool TryGetPlatform(Vector2Int index, out Platform result);
    }
}