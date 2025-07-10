using UnityEngine;

namespace MyCode
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private PlayerSettings _settings;

        public Player Player { get; private set; }

        private IGridMap _gridMap;

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.Dead, SetPlayerSpawnPos);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.Dead, SetPlayerSpawnPos);
        }

        public void SetupMap(IGridMap gridMap)
        {
            _gridMap = gridMap;
        }

        public void SetPlayerSpawnPos()
        {
            Player.Setup(_gridMap, _gridMap.FindPlatform(PlatformType.PlayerSpawn).GridIndex, _settings);
            Vector3 spawnPos = _gridMap.FindPlatform(PlatformType.PlayerSpawn).transform.position;
            spawnPos.y += _settings.OffsetY;
            Player.transform.position = spawnPos;
        }

        public void CreatePlayer()
        {
            Player = Instantiate(_settings.PlayerPrefab);
            SetPlayerSpawnPos();
        }
    }
}