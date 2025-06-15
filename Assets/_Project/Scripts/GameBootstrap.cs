using UnityEngine;

namespace MyCode
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private PlayerManager _playerManager;

        private void Start()
        {
            _gridManager.CreateGrid();
            _playerManager.CreatePlayer();
        }
    }
}