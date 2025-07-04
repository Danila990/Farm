using UnityEngine;

namespace MyCode
{
    public class PlayerRoot : Singleton<PlayerRoot>
    {
        [SerializeField] private PlayerUnit _unitPrefab;
        [SerializeField] private Vector3 _spawnOffset;

        public PlayerUnit Unit { get; private set; }

        private IGridMap _gridMap;

        public void Initialize(IGridMap gridMap)
        {
            _gridMap = gridMap;
            CreatePlayer();
        }

        public void Startable()
        {
            SetPlayerSpawnPos();
            Unit.Startable(_gridMap, _gridMap.FindPlatform(PlatformType.PlayerSpawn).GridIndex);
        }

        private void SetPlayerSpawnPos()
        {
            Platform playerSpawn = _gridMap.FindPlatform(PlatformType.PlayerSpawn);
            Unit.transform.position = _spawnOffset + playerSpawn.transform.position;
        }

        private void CreatePlayer()
        {
            Unit = Instantiate(_unitPrefab);
        }
    }
}
