using Code;
using UnityEngine;

namespace Code
{
    public class GridController : MonoBehaviour, IGridService
    {
        [SerializeField] private GridMap _map;

        public void CreateGrid()
        {
            _map = Instantiate(_map);
        }

        public IGridMap GetGridMap()
        {
            return _map;
        }

        public void RestartGrid()
        {
            
        }
    }
}