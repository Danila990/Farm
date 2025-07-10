using System;
using UnityEngine;

namespace MyCode
{
    [Serializable]
    public struct PlayerSettings
    {
        public Player PlayerPrefab;
        [Range(0,1)] public float RotateDuration;
        [Range(0.2f, 1)] public float JumpDuration;
        public float JumpHeight;
        public float OffsetY;
        [Range(0.2f, 1)] public float AwaitDuration;
    }
}