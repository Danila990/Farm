using System;
using UnityEngine;

namespace MyCode
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GridController _gridController;
        [SerializeField] private PlayerController _playerController;

        private void Start()
        {
            Initialize();
            Startable();
        }

        private void Startable()
        {
            _playerController.Player.Moved();
        }

        private void Initialize()
        {
            _gridController.CreateGrid();
            _playerController.SetupMap(_gridController.GridMap);
            _playerController.CreatePlayer();
        }
    }
}