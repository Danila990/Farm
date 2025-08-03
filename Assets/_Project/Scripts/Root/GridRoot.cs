using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class GridRoot : MonoBehaviour
    {
        [SerializeField] private string _pathGrid = "GridMaps/GridMap_";

        private IGridMap _gridMap;

        public async Task Initializable()
        {
            _gridMap = await AssetFactory.Create<GridMap>(_pathGrid);
            ServiceLocator.Register(_gridMap);
        }
    }
}