using System;
using UnityEngine;

namespace MyCode
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] private GridLine[] _grid;

        public void SetupMap(GridLine[] grid)
        {
            _grid = grid;
        }
    }

    [Serializable]
    public struct GridLine
    {
        public Platform[] platforms;
    }
}