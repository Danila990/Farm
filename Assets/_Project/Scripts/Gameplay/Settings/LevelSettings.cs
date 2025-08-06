using ProjectCode;
using System;
using UnityEngine;

namespace Project1
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "SO/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        public PlayerInfo PlayerInfo;
        public GridInfo GridInfo;
    }

    [Serializable]
    public class GridInfo
    {
        public GridMap MapPrefab;
    }

    [Serializable]
    public class PlayerInfo
    {
        public Player PlayerPrefab;
        public float RotateSpeeed = 600f;
        public float SpeedArrow = 1000f;
        public float JumpHeight = 1f;
        public float JumpDuration = 0.5f;
        public float OffsetPosY;
    }
}