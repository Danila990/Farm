using ProjectCode;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Project1
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private string _pathSettings;

        private LevelSettings _settings;
        private GridController _gridController;
        private PlayerController _playerController;
        private InputController _inputController;

        private async void Awake()
        {
            ServiceLocator.Clear();
            EventBus.Clear();
            await Init();
            Registe();
            Startable();
        }

        private void Registe()
        {
            ServiceLocator.Register(_settings);
            ServiceLocator.Register(_gridController);
            ServiceLocator.Register(_inputController);
            ServiceLocator.Register(_playerController);
        }

        private async Task Init()
        {
            _settings = await AssetLoader.LoadInstantiate<LevelSettings>(_pathSettings);

            _gridController = new GameObject("GridController").AddComponent<GridController>();
            _gridController.Initialize(_settings);
            
            _inputController = new GameObject("InputController").AddComponent<InputController>();
            await _inputController.Initialize();
            
            _playerController = new GameObject("PlayerController").AddComponent<PlayerController>();
            _playerController.Initialize(_settings);
        }

        private void Startable()
        {
            _inputController.Startable();
            _playerController.Startable();
        }
    }
}