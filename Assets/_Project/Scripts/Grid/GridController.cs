using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MyCode
{
    public class GridController : MonoBehaviour
    {
        public event Action<GridMap> ChangeMap;

        [SerializeField] private GridMap[] _maps;

        private GridMap _currentMap;
        private int _mapIndex;

        public GridMap GetGridMap()
        {
            return _currentMap;
        }

        public void ActivateFirstGridMap()
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
            ChangeMap?.Invoke(_currentMap);
        }
    }
}