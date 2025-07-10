using UnityEngine;

namespace MyCode
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private PlayerSettings _settings;

        public Player Player { get; private set; }

        private IGridMap _gridMap;

        public void SetupMap(IGridMap gridMap)
        {
            _gridMap = gridMap;
        }

        public void SetPlayerSpawnPos()
        {
            Vector3 spawnPos = _gridMap.FindPlatform(PlatformType.PlayerSpawn).transform.position;
            spawnPos.y += _settings.OffsetY;
            Player.transform.position = spawnPos;
        }

        public void CreatePlayer()
        {
            Player = Instantiate(_settings.PlayerPrefab);
            SetPlayerSpawnPos();
            Player.Setup(_gridMap, _gridMap.FindPlatform(PlatformType.PlayerSpawn).GridIndex, _settings);
        }
    }
}