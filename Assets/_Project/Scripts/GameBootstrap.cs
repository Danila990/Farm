using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyCode
{
    public class GameBootstrap : IInitializable
    {
        private readonly GridController _gridController;
        private readonly GridNavigation _gridNavigator;
        private readonly PlayerController _playerController;

        public GameBootstrap(GridController gridController, PlayerController playerController, GridNavigation gridNavigator)
        {
            _gridController = gridController;
            _playerController = playerController;
            _gridNavigator = gridNavigator;
        }

        public void Initialize()
        {
            _gridController.Initialize();
            _gridNavigator.Initialize();
            _playerController.Initialize();
        }
    }
}