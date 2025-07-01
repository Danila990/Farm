using UnityEngine;

namespace MyCode
{
    public class GridRoot : Singleton<GridRoot>
    {
        [SerializeField] private GridMap _map;

        public IGridMap GridMap => _map;
        
        public void Initialize()
        {
            CreateGrid();
        }

        public void Startable()
        {
            
        }

        private void CreateGrid()
        {
            _map = Instantiate(_map);
        }
    }
}
