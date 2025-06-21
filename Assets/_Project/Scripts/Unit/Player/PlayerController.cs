using UnityEngine;
using VContainer;


namespace MyCode
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerUnit _prefab;
        [SerializeField] private Vector3 _offset;

        private InjectService _injector;
        private GameGrid _gameGrid;

        public PlayerUnit Player { get; private set; }
        public Platform Spawn { get; private set; }

        [Inject]
        public void Constructor(InjectService injectService, GameGrid gridController)
        {
            _injector = injectService;
            _gameGrid = gridController;
        }

        public void Initialize()
        {
            Player = _injector.CreateInject(_prefab);
            _gameGrid.OnChangeMap += OnChangeMap;
        }

        public void Startable()
        {
            Player.StartMove();
        }

        private void OnChangeMap(IGridMap gridMap)
        {
            Spawn = gridMap.FindPlatform(PlatformType.PlayerSpawn);
            Player.transform.position = _offset + Spawn.transform.position;
            Player.SetupUnit(gridMap, Spawn.GridIndex);
        }

        private void OnDestroy()
        {
            _gameGrid.OnChangeMap -= OnChangeMap;
        }
    }
}