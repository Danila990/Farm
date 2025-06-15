using MyCode;
using System;
using UnityEditor;
using UnityEngine;

namespace MyCode.Constructor 
{
    public class GridConstructor : MonoBehaviour
    {
#if UNITY_EDITOR
        [HideInInspector] public ConstructorLine[] linesY;
        [SerializeField] private MoodArray<Platform> _container;
        public float offsetPlatform = 1f;
        public GridMap gridMap;

        private void OnValidate()
        {
            if (linesY == null || linesY.Length == 0)
            {
                linesY = new ConstructorLine[1]
                {
                    new ConstructorLine()
                    {
                        lineX = new PlatformType[1]{ PlatformType.Default }
                    }
                };
            }
        }


        #region Constructor
        public void AddYGrid()
        {
            ConstructorLine[] newGridLines = new ConstructorLine[linesY.Length + 1];
            linesY.CopyTo(newGridLines, 0);
            newGridLines[linesY.Length] = new ConstructorLine() { lineX = new PlatformType[linesY[0].lineX.Length] };
            linesY = newGridLines;
        }

        public void RemoveYGrid()
        {
            if (linesY.Length <= 1)
                return;

            ConstructorLine[] newGridLines = new ConstructorLine[linesY.Length - 1];
            Array.Copy(linesY, newGridLines, newGridLines.Length);
            linesY = newGridLines;
        }

        public void RemoveXGrid()
        {
            if (linesY[0].lineX.Length <= 1)
                return;

            for (int i = 0; i < linesY.Length; i++)
            {
                PlatformType[] newRow = new PlatformType[linesY[i].lineX.Length - 1];
                Array.Copy(linesY[i].lineX, newRow, newRow.Length);
                linesY[i].lineX = newRow;
            }
        }

        public void AddXGrid()
        {
            for (int i = 0; i < linesY.Length; i++)
            {
                PlatformType[] newRow = new PlatformType[linesY[i].lineX.Length + 1];
                linesY[i].lineX.CopyTo(newRow, 0);
                linesY[i].lineX = newRow;
            }
        }

        public void ResetGrid()
        {
            linesY = new ConstructorLine[1] { new ConstructorLine() { lineX = new PlatformType[1] { PlatformType.Default } } };
        }
        #endregion

        #region CreateGrid

        public void CreatePrefab()
        {
            if (gridMap == null)
                return;

            string localPath = "Assets/_Project/Prefabs/Grid/" + gridMap.gameObject.name + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gridMap.gameObject, localPath);
            Debug.Log("Create prefab", prefab);
        }


        public PlatformType[,] ConvertGrid()
        {
            PlatformType[,] platforms = new PlatformType[linesY.Length, linesY[0].lineX.Length];
            for (int i = 0; i < linesY.Length; i++)
            {
                for (int j = 0; j < linesY[i].lineX.Length; j++)
                {
                    platforms[i, j] = linesY[i].lineX[j];
                }
            }

            return platforms;
        }

        public void CreatGrid()
        {
            if (gridMap != null)
                DestroyGrid();


            PlatformType[,] platfromTypes = ConvertGrid();
            Vector2Int gridSize = new Vector2Int(platfromTypes.GetLength(0), platfromTypes.GetLength(1));
            Vector3 spawnOffset = MiddleOffest(offsetPlatform, gridSize);
            gridMap = new GameObject($"GridMap({gridSize.x}, {gridSize.y})").AddComponent<GridMap>();
            ArrayLine<Platform>[] grid = new ArrayLine<Platform>[gridSize.x];
            for (int x = 0; x < gridSize.x; x++)
            {
                grid[x].LineValues = new Platform[gridSize.y];
                for (int y = 0; y < gridSize.y; y++)
                {
                    Platform platform = CreatePlatform(platfromTypes[x, y], y, x, spawnOffset);
                    grid[x].LineValues[y] = platform;
                    platform.SetupPlatform(new Vector2Int(x, y));
                }
            }

            gridMap.SetupMap(grid);
        }

        public void DestroyGrid()
        {
            if (gridMap == null)
                return;

            DestroyImmediate(gridMap.gameObject);
        }

        private Platform CreatePlatform(PlatformType platformType, int gridHeightX, int gridWidthY, Vector3 spawnOffset)
        {
            string namePlatform = $"Platform_{platformType}";
            Platform platform = Instantiate(_container.Get<Platform>(namePlatform));
            platform.transform.position = new Vector3(gridHeightX * offsetPlatform, 0, gridWidthY * offsetPlatform) - spawnOffset;
            platform.transform.parent = gridMap.transform;
            return platform;
        }

        private Vector3 MiddleOffest(float platformOffset, Vector2Int gridSize)
        {
            float gridWidth = gridSize.y * platformOffset - platformOffset;
            float gridHeight = gridSize.x * platformOffset - platformOffset;
            return new Vector3(gridWidth, 0, gridHeight) / 2;
        }
        #endregion
#endif
    }

#if UNITY_EDITOR
    [Serializable]
    public class ConstructorLine
    {
        public PlatformType[] lineX;
    }
#endif
}