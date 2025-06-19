using UnityEngine;

namespace MyCode 
{
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] private GridController _gridController;

        public GridMap CurrentMap => _gridController.GetGridMap();

        public void CreateGrid()
        {
            
        }

        public void ResetGrid()
        {

        }
    }
}