using ProjectCode;
using System;
using UnityEditor;
using UnityEngine;

namespace ProjectCode.Constructor 
{
    public class GridConstructor : MonoBehaviour
    {
#if UNITY_EDITOR

        public float OffsetPlatform = 1f;
        public GridMap GridMap;
        public string PathGridMap = "Content/Prefabs/Grid";

        [HideInInspector] public ConstructorLine[] LinesX;

        [SerializeField] private MoodArray<Platform> _container;

        private void OnValidate()
        {
            if (LinesX == null || LinesX.Length == 0)
            {
                LinesX = new ConstructorLine[1]
                {
                    new ConstructorLine()
                    {
                        lineY = new PlatformType[1]{ PlatformType.Base }
                    }
                };
            }
        }


        #region ConstructorEditor
        /*public void AddYGrid()
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
        }*/

        public void CreateGrid(Vector2Int size)
        {
            LinesX = new ConstructorLine[size.x];
            for (int i = 0; i < size.x; i++)
            {
                LinesX[i] = new ConstructorLine();
                LinesX[i].lineY = new PlatformType[size.y];
            }
        }

        public void ResetGrid()
        {
            LinesX = new ConstructorLine[1] { new ConstructorLine() { lineY = new PlatformType[1] { PlatformType.Base } } };
        }

        public void ResetPlatformsTypes()
        {
            for (int i = 0; i < LinesX.Length; i++)
            {
                for (int j = 0; j < LinesX[0].lineY.Length; j++)
                {
                    LinesX[i].lineY[j] = PlatformType.Base;
                }
            }
        }

        public void CreatePrefab()
        {
            if (GridMap == null)
                CreatGrid();

            string localPath = $"Assets/{PathGridMap}/{GridMap.gameObject.name}.prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(GridMap.gameObject, localPath);
            Debug.Log("Create prefab", prefab);
            DestroyGrid();
        }

        #endregion

        #region CreateGrid

        public PlatformType[,] ConvertGrid()
        {
            PlatformType[,] platforms = new PlatformType[LinesX.Length, LinesX[0].lineY.Length];
            for (int i = 0; i < LinesX.Length; i++)
            {
                for (int j = 0; j < LinesX[i].lineY.Length; j++)
                {
                    platforms[i, j] = LinesX[i].lineY[j];
                }
            }

            return platforms;
        }

        public void CreatGrid()
        {
            if (GridMap != null)
                DestroyGrid();

            PlatformType[,] platfromTypes = ConvertGrid();
            Vector2Int gridSize = new Vector2Int(platfromTypes.GetLength(0), platfromTypes.GetLength(1));
            Vector3 spawnOffset = MiddleOffest(OffsetPlatform, gridSize);
            GridMap = new GameObject($"GridMap_").AddComponent<GridMap>();
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

            GridMap.SetupMap(grid);
        }

        public void DestroyGrid()
        {
            if (GridMap == null)
                return;

            DestroyImmediate(GridMap.gameObject);
        }

        private Platform CreatePlatform(PlatformType platformType, int gridHeightX, int gridWidthY, Vector3 spawnOffset)
        {
            string namePlatform = $"Platform_{platformType}";
            Platform platform = Instantiate(_container.Get<Platform>(namePlatform));
            platform.transform.position = new Vector3(gridHeightX * OffsetPlatform, 0, gridWidthY * OffsetPlatform) - spawnOffset;
            platform.transform.parent = GridMap.transform;
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