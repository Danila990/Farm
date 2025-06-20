using System;
using UnityEditor;
using UnityEngine;

namespace MyCode.Constructor
{
    [CustomEditor(typeof(GridConstructor))]
    public class GridComstructorEditor : Editor
    {
        private GridConstructor _constructor;

        public override void OnInspectorGUI()
        {
            _constructor = (GridConstructor)target;
            base.OnInspectorGUI();

            BaseMidlleText("Preview Grid", 3);
            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            BaseButton("Add Row X", _constructor.AddXGrid);
            BaseButton("Remove Row X", _constructor.RemoveXGrid);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            BaseButton("Add Line Y", _constructor.AddYGrid);
            BaseButton("Remove Line Y", _constructor.RemoveYGrid);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            BaseButton("Reset Grid", _constructor.ResetGrid);
            BaseButton("Reset Platform Types", _constructor.ResetPlatforms);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);

            BaseMidlleText("Grid map", 3);
            EditorGUILayout.BeginHorizontal();
            BaseButton("Create Grid map", _constructor.CreatGrid);
            BaseButton("Destroy Grid map", _constructor.DestroyGrid);
            EditorGUILayout.EndHorizontal();

            BaseMidlleText("Prefab", 3);
            BaseButton("Create new prefab", _constructor.CreatePrefab);

            PrinSizeGrid();
            DrawPreviewGrid();
        }

        private void PrinSizeGrid()
        {
            Vector2Int sizeGrid = new Vector2Int(_constructor.linesY[0].lineX.Length, _constructor.linesY.Length);
            BaseMidlleText($"Grid: X - {sizeGrid.x}, Y - {sizeGrid.y}", 10);
        }

        private void DrawPreviewGrid()
        {
            for (int i = _constructor.linesY.Length - 1; i >= 0; i--)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                if (_constructor.linesY[i] != null)
                {
                    for (int j = 0; j < _constructor.linesY[i].lineX.Length; j++)
                    {
                        Color enumColor = GetPlatfromColor(_constructor.linesY[i].lineX[j]);
                        Rect rect = GUILayoutUtility.GetRect(20, 20);
                        EditorGUI.DrawRect(rect, enumColor);
                        GUI.color = Color.white;

                        _constructor.linesY[i].lineX[j] = (PlatformType)EditorGUILayout.EnumPopup(_constructor.linesY[i].lineX[j]);
                        GUI.color = Color.white;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        /*private void DrawPreviewGrid()
        {
            for (int i = 0; i < _constructor.linesY.Length; i++)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                if (_constructor.linesY[i] != null)
                {
                    for (int j = 0; j < _constructor.linesY[i].lineX.Length; j++)
                    {
                        Color enumColor = GetPlatfromColor(_constructor.linesY[i].lineX[j]);
                        Rect rect = GUILayoutUtility.GetRect(20, 20);
                        EditorGUI.DrawRect(rect, enumColor);
                        GUI.color = Color.white;

                        _constructor.linesY[i].lineX[j] = (PlatformType)EditorGUILayout.EnumPopup(_constructor.linesY[i].lineX[j]);
                        GUI.color = Color.white;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }*/

        private void BaseMidlleText(string text, int space)
        {
            EditorGUILayout.Space(space);
            GUIStyle centeredStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter

            };

            EditorGUILayout.LabelField(text, centeredStyle);
        }

        private void BaseButton(string text, Action action)
        {
            if (GUILayout.Button(text))
                action();
        }

        private Color GetPlatfromColor(PlatformType platformType)
        {
            switch (platformType)
            {
                case PlatformType.Default:
                    return Color.white;

                case PlatformType.PlayerSpawn:
                    return Color.green;

                case PlatformType.Empty:
                    return Color.black;

                case PlatformType.Finish:
                    return Color.blue;

                case PlatformType.Coin:
                    return Color.yellow;

                default:
                    return Color.white;
            }
        }
    }
}