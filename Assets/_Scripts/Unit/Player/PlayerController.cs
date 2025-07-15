using UnityEngine;
using VContainer;

namespace Code
{
    public class PlayerController : MonoBehaviour, IPlayerService
    {
        [SerializeField] private PlayerUnit _prefab;
        [SerializeField] private Vector3 _startOffset;

        private PlayerUnit _player;
        [Inject] private IGridService _gridService;
        [Inject] private InjectService _injector;

        public void CreatePlayer()
        {
            _player = _injector.Create(_prefab);
        }

        public void DeadPlayer()
        {
            SetPlayerSpawnPos();
        }

        public PlayerUnit GetPlayer()
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
            Vector3 spawnPos = _gridService.GetGridMap().FindPlatform(PlatformType.PlayerSpawn).transform.position;
            spawnPos += _startOffset;
            _player.transform.position = spawnPos;
        }
    }
}