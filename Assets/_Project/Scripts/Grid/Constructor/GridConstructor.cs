using MyCode;
using System;
using UnityEditor;
using UnityEngine;

namespace MyCode.Constructor 
{
    public class GridConstructor : MonoBehaviour
    {
#if UNITY_EDITOR
        [HideInInspector] public ConstructorLine[] linesX;
        [SerializeField] private MoodArray<Platform> _container;
        public float offsetPlatform = 1f;
        public GridMap gridMap;

        private void OnValidate()
        {
            if (linesX == null || linesX.Length == 0)
            {
                linesX = new ConstructorLine[1]
                {
                    new ConstructorLine()
                    {
                        lineY = new PlatformType[1]{ PlatformType.Default }
                    }
                };
            }
        }


        #region Constructor
        public void AddYGrid()
        {
            ConstructorLine[] newGridLines = new ConstructorLine[linesX.Length + 1];
            linesX.CopyTo(newGridLines, 0);
            newGridLines[linesX.Length] = new ConstructorLine() { lineY = new PlatformType[linesX[0].lineY.Length] };
            linesX = newGridLines;
        }

        public void RemoveYGrid()
        {
            if (linesX.Length <= 1)
                return;

            ConstructorLine[] newGridLines = new ConstructorLine[linesX.Length - 1];
            Array.Copy(linesX, newGridLines, newGridLines.Length);
            linesX = newGridLines;
        }

        public void RemoveXGrid()
        {
            if (linesX[0].lineY.Length <= 1)
                return;

            for (int i = 0; i < linesX.Length; i++)
            {
                PlatformType[] newRow = new PlatformType[linesX[i].lineY.Length - 1];
                Array.Copy(linesX[i].lineY, newRow, newRow.Length);
                linesX[i].lineY = newRow;
            }
        }

        public void AddXGrid()
        {
            for (int i = 0; i < linesX.Length; i++)
            {
                PlatformType[] newRow = new PlatformType[linesX[i].lineY.Length + 1];
                linesX[i].lineY.CopyTo(newRow, 0);
                linesX[i].lineY = newRow;
            }
        }

        public void ResetGrid()
        {
            linesX = new ConstructorLine[1] { new ConstructorLine() { lineY = new PlatformType[1] { PlatformType.Default } } };
        }

        public void ResetPlatforms()
        {
            for (int i = 0; i < linesX.Length; i++)
            {
                for (int j = 0; j < linesX[0].lineY.Length; j++)
                {
                    linesX[i].lineY[j] = PlatformType.Default;
                }
            }
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
            PlatformType[,] platforms = new PlatformType[linesX.Length, linesX[0].lineY.Length];
            for (int i = 0; i < linesX.Length; i++)
            {
                for (int j = 0; j < linesX[i].lineY.Length; j++)
                {
                    platforms[i, j] = linesX[i].lineY[j];
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
                    Platform platform = CreatePlatform(platfromTypes[x, y], x, y, spawnOffset);
                    grid[x].LineValues[y] = platform;
                    platform.SetupPlatform(new Vector2Int(x, y));
                    platform.gameObject.name = $"{x}, {y}";
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
        public PlatformType[] lineY;
    }
#endif
}