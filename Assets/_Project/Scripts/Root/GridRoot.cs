using UnityEngine;

namespace MyCode
{
    public class GridRoot : Singleton<GridRoot>
    {
        [field: SerializeField] public MapController MapController { get; private set; }

        public void Initialize()
        {
            MapController.Initialize();
        }

        public void Startable()
        {
            MapController.ActivateFirstMap();
        }
    }
}
