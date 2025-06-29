using System;
using UnityEngine;

namespace MyCode
{
    public class PlayerRoot : Singleton<PlayerRoot>
    {
        [SerializeField] private PlayerUnit _unitPrefab;

        public PlayerUnit Player { get; private set; }

        private Platform _playerSpawn;

        private void OnEnable() => EventBus.Subscribe<IGridMap>(OnUpdateGridMap, 5);

        private void OnDisable() =>  EventBus.Unsubscribe<IGridMap>(OnUpdateGridMap);

        public void Initialize()
        {
            CreatePlayer();
        }

        public void Startable()
        {
            Player.Startable();
        }

        private void CreatePlayer()
        {
            Player = Instantiate(_unitPrefab);
        }

        private void OnUpdateGridMap(IGridMap map)
        {
            _playerSpawn = map.FindPlatform(PlatformType.PlayerSpawn);
            Player.transform.position = new Vector3(0, 0.5f, 0) + _playerSpawn.transform.position;
        }
    }
}
