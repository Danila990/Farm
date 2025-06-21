using System;
using Unity.Mathematics;
using UnityEngine;

namespace MyCode
{
    public class GameGrid : MonoBehaviour
    {
        public event Action<IGridMap> OnChangeMap;

        [SerializeField] private GridMap[] _maps;

        private int _mapIndex;
        private GridMap _currentMap;

        public void Initialize()
        {
            for (int i = 0; i < _maps.Length; i++)
            {
                GridMap map = Instantiate(_maps[i]);
                map.gameObject.SetActive(false);
                _maps[i] = map;
            }
        }

        public IGridMap GetCurrentMap()
        {
            return _currentMap;
        }

        public void Startable()
        {
            ActivateMap(0);
        }

        public void PreviousGridMap()
        {
            _mapIndex--;
            _mapIndex = math.clamp(_mapIndex, 0, _maps.Length);
            ActivateMap(_mapIndex);
        }

        public void NextGridMap()
        {
            _mapIndex++;
            if (_mapIndex >= _maps.Length)
            {
                Debug.Log("end");
            }
            else
                ActivateMap(_mapIndex);
        }

        private void ActivateMap(int mapIndex)
        {
            if (_currentMap != null)
                _currentMap.gameObject.SetActive(false);

            _mapIndex = mapIndex;
            _currentMap = _maps[_mapIndex];
            _currentMap.gameObject.SetActive(true);
            OnChangeMap?.Invoke(_currentMap);
        }
    }
}