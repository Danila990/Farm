using UnityEngine;

namespace MyCode
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private PlayerManager _playerManager;

        private void Start()
        {
            new GameObject("Event Bus").AddComponent<EventBus>();
            _gridManager.CreateGrid();
            _playerManager.CreatePlayer();
        }
    }
}