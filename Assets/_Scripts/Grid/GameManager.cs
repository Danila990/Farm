using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code
{
    public class GameManager : MonoBehaviour, IInitializable
    {
        private IPlayerService _playerService;
        private IGridService _gridService;

        [Inject]
        public void Inject(IPlayerService playerService, IGridService gridService)
        {
            _playerService = playerService;
            _gridService = gridService;
        }

        public void Initialize()
        {
            Startable();
        }

        private void Startable()
        {
            _gridService.CreateGrid();
            _playerService.CreatePlayer();
            _playerService.GetPlayer().Moved();
            _playerService.StartPlayer();
        }
    }
}