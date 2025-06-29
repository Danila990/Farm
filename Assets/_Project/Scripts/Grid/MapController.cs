using UnityEngine;

namespace MyCode
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private GridMap[] _maps;

        private GridMap _currentMap;
        private int _mapIndex;

        public IGridMap CurrentMap => _currentMap;

        private void OnEnable()
        {
            EventBus.Subscribe<FinishGrid>(OnFinish);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<FinishGrid>(OnFinish);
        }

        public void Initialize()
        {
            CreateMaps();
        }

        public void ActivateFirstMap()
        {
            ActivateMap(0);
        }

        [ContextMenu("NextMap")]
        public void NextMap()
        {
            _mapIndex++;
            if (_mapIndex >= _maps.Length)
            {
                Debug.Log("Level complete");
                return;
            }

            ActivateMap(_mapIndex);
        }

        [ContextMenu("Preview Map")]
        public void PreviewMap()
        {
            _mapIndex--;
            if (_mapIndex < 0)
            {
                Debug.Log("Level loss");
                return;
            }

            ActivateMap(_mapIndex);
        }

        private void OnFinish(FinishGrid finishGrid)
        {
            NextMap();
        }

        private void ActivateMap(int number)
        {
            if(_currentMap != null)
                _currentMap.gameObject.SetActive(false);

            _currentMap = _maps[number];
            _currentMap.gameObject.SetActive(true);
            EventBus.Publish<IGridMap>(_currentMap);
        }

        private void CreateMaps()
        {
            for (int i = 0; i < _maps.Length; i++)
            {
                GridMap gridMap = Instantiate(_maps[i]);
                gridMap.gameObject.SetActive(false);
                _maps[i] = gridMap;
            }
        }
    }
}