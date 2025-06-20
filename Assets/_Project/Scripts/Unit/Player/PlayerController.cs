using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyCode
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerScriptable _playerScriptable;

        private InjectService _injector;
        private GridController _gridController;

        public PlayerUnit Player { get; private set; }
        public Platform Spawn { get; private set; }

        [Inject]
        public void Constructor(InjectService injectService, GridController gridController)
        {
            _injector = injectService;
            _gridController = gridController;
        }

        public void Initialize()
        {
            Spawn = _gridController.GetCurrentMap().FindPlatform(PlatformType.PlayerSpawn);
            CreatePlayer();
            RespawnPlayer();
        }

        private void RespawnPlayer()
        {
            Player.transform.position = _playerScriptable.PlayerInfo.PosOffset + Spawn.transform.position;
        }

        private void CreatePlayer()
        {
            Player = _injector.CreateInject(_playerScriptable.PlayerInfo.Prefab);
        }
    }
}