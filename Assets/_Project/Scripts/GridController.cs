using Unity.VisualScripting;
using UnityEngine;

namespace MyCode
{
    public class GridController : Singleton<GridController>
    {
        [SerializeField] private GridMap _map;

        public IGridMap GridMap => _map;

        public void CreateGrid()
        {
            _map = Instantiate(_map);
        }
    }
}