using MyCode;
using System;
using UnityEngine;

namespace MyCode
{
    [CreateAssetMenu(menuName = "GridMapSetting", fileName = nameof(GridMapSetting))]
    public class GridMapSetting : ScriptableObject
    {
        [HideInInspector] private PlatformLineInfo[] _platformLineInfos;

        [field: SerializeField] public float platformOffset { get; private set; } = 1f;

        public void SetLines(PlatformLineInfo[] newlines) => _platformLineInfos = newlines;

        public PlatformLineInfo[] GetLines() => _platformLineInfos;

        public PlatformType[,] GetPlatformLines()
        {
            PlatformType[,] platforms = new PlatformType[_platformLineInfos.Length, PlatformsSizeY()];
            for (int i = 0; i < _platformLineInfos.Length; i++)
            {
                for (int j = 0; j < _platformLineInfos[i].platformTypes.Length; j++)
                {
                    platforms[i, j] = _platformLineInfos[i].platformTypes[j];
                }
            }

            return platforms;
        }

        private int PlatformsSizeY()
        {
            int sizeY = 0;
            foreach (var platformInfo in _platformLineInfos)
            {
                int lengthLine = platformInfo.platformTypes.Length;
                if (sizeY < lengthLine)
                    sizeY = lengthLine;
            }

            return sizeY;
        }
    }

    [Serializable]
    public class PlatformLineInfo
    {
        public PlatformType[] platformTypes;
    }
}