using Code;
using UnityEngine;
using VContainer;

namespace Code
{
    public class PlayerController : MonoBehaviour, IPlayerService
    {
        [SerializeField] private PlayerSettings _settings;

        private Player _player;
        [Inject] private IGridService _gridService;

        public void CreatePlayer()
        {
            _player = Instantiate(_settings.PlayerPrefab);
        }

        public void DeadPlayer()
        {
            SetPlayerSpawnPos();
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public void RestartPlayer()
        {
            SetPlayerSpawnPos();
        }

        public void StartPlayer()
        {
            SetPlayerSpawnPos();
        }

        public void StopPlayer()
        {
            
        }

        private void SetPlayerSpawnPos()
        {
            IGridMap map = _gridService.GetGridMap();
            _player.Setup(map, map.FindPlatform(PlatformType.PlayerSpawn).GridIndex, _settings);
            Vector3 spawnPos = map.FindPlatform(PlatformType.PlayerSpawn).transform.position;
            spawnPos.y += _settings.OffsetY;
            _player.transform.position = spawnPos;
        }
    }
}