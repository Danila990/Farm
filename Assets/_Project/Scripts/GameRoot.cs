using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyCode
{
    public class GameRoot : IInitializable, IStartable
    {
        private readonly GameGrid _gridController;
        private readonly PlayerController _playerController;

        public GameRoot(GameGrid gridController, PlayerController playerController)
        {
            _gridController = gridController;
            _playerController = playerController;
        }

        public void Initialize()
        {
            _gridController.Initialize();
            _playerController.Initialize();
        }

        public void Start()
        {
            _gridController.Startable();
            _playerController.Startable();
        }
    }
}