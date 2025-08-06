using Project1;
using UnityEngine;

namespace ProjectCode
{
    public class GridController : MonoBehaviour
    {
        private GridMap _gridMap;

        public void Initialize(LevelSettings levelSettings)
        {
            _gridMap = Instantiate(levelSettings.GridInfo.MapPrefab);
            ServiceLocator.Register<IGridMap>(_gridMap);
        }
    }
}