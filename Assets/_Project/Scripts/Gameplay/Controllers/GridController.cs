using UnityEngine;

namespace ProjectCode
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GridMap _gridMap;

        public void Initialize()
        {
            _gridMap = Instantiate(_gridMap);
            ServiceLocator.Register<IGridMap>(_gridMap);
        }
    }
}