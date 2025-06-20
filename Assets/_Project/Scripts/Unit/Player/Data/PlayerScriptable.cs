using System;
using UnityEngine;

namespace MyCode
{
    [CreateAssetMenu(fileName = "PlayerScriptable")]
    public class PlayerScriptable : ScriptableObject
    {
        [SerializeField] private PlayerScriptableInfo _playerInfo;

        public PlayerScriptableInfo PlayerInfo => _playerInfo;
    }

    [Serializable]
    public struct PlayerScriptableInfo
    {
        public PlayerUnit Prefab;
        public float MoveSpeed;
        public float RotateSpeed;
        public int Health;
        public Vector3 PosOffset;
    }
}
