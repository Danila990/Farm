using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GridController _gridController;
        [SerializeField] private InputController _inputController;

        private async void Awake()
        {
            RegistServices();
            await CoreInitialize();
            Startable();
        }

        private void Startable()
        {
            _inputController.Startable();
            _playerController.Startable();
        }

        private void RegistServices()
        {
            ServiceLocator.Register(_gridController);
            ServiceLocator.Register(_inputController);
            ServiceLocator.Register(_playerController);
        }

        private async Task CoreInitialize()
        {
            _gridController.Initialize();
            await _inputController.Initialize();
            _playerController.Initialize();
        }

        private void OnDestroy()
        {
            ServiceLocator.Clear();
            EventBus.Clear();
        }
    }
}