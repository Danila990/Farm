using System.Linq;
using UnityEngine;

namespace MyCode
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] private DataContainer _container;
        [SerializeField] private GridMapSetting _gridInfo;

        private Platform[,] _platforms;
        private float _platformOffset => _gridInfo.platformOffset;

        private void Start()
        {
            CreatGrid();
        }

        public void CreatGrid()
        {
            PlatformType[,] platfromTypes = _gridInfo.GetPlatformLines();
            Vector2Int gridSize = new Vector2Int(platfromTypes.GetLength(0), platfromTypes.GetLength(1));
            Vector3 spawnOffset = MiddleOffest(_platformOffset, gridSize);
            _platforms = new Platform[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Platform platform = CreatePlatform(platfromTypes[x, y], x, y, spawnOffset);
                    _platforms[x, y] = platform;
                    platform.SetupPlatform(new Vector2Int(x, y));
                }
            }
        }

        public void DestroyGrid()
        {

        }

        public Platform[,] GetGrid()
        {
            if(_gridInfo == null)
                CreatGrid();

            return _platforms;
        }

        private Platform CreatePlatform(PlatformType platformType, int x, int y, Vector3 spawnOffset)
        {
            string namePlatform = $"Platform_{platformType}";
            Platform platform = Instantiate(_container.Get<Platform>(namePlatform));
            platform.transform.position = new Vector3(x * _platformOffset, 0, y * _platformOffset) - spawnOffset;
            platform.transform.parent = transform;
            return platform;
        }

        private Vector3 MiddleOffest(float platformOffset, Vector2Int gridSize)
        {
            float gridWidth = gridSize.x * platformOffset - platformOffset;
            float gridHeight = gridSize.y * platformOffset - platformOffset;
            return new Vector3(gridWidth, 0, gridHeight) / 2;
        }
    }
}