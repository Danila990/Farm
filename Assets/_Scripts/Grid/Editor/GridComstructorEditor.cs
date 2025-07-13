using System;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Code.Constructor
{
    [CustomEditor(typeof(GridConstructor))]
    public class GridComstructorEditor : Editor
    {
        private GridConstructor _constructor;
        private Vector2Int _sizeGrid;

        public override void OnInspectorGUI()
        {
            _constructor = (GridConstructor)target;
            base.OnInspectorGUI();

            BaseMidlleText("Preview Grid", 3);
            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            _sizeGrid = EditorGUILayout.Vector2IntField("New Size Grid", _sizeGrid);
            BaseButton("Create Grid", () =>
            {
                if (_sizeGrid == Vector2Int.zero)
                    return;

                _constructor.CreateGrid(_sizeGrid);
            });
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            BaseButton("Create Preview grid", _constructor.CreatGrid);
            BaseButton("Destroy Preview grid", _constructor.DestroyGrid);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            BaseButton("Reset Grid", _constructor.ResetGrid);
            BaseButton("Reset Platform Types", _constructor.ResetPlatformsTypes);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);

            BaseButton("Create Prefab Grid map", _constructor.CreatePrefab);

            PrinSizeGrid();
            DrawPreviewGrid();
        }

        private void PrinSizeGrid()
        {
            Vector2Int sizeGrid = new Vector2Int(_constructor.LinesX.Length, _constructor.LinesX[0].lineY.Length);
            BaseMidlleText($"Grid: X - {sizeGrid.x}, Y - {sizeGrid.y}", 10);
        }

        private void DrawPreviewGrid()
        {
            for (int i = 0; i < _constructor.LinesX.Length; i++)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();

                if (_constructor.LinesX[i] != null)
                {
                    for (int j = 0; j < _constructor.LinesX[i].lineY.Length; j++)
                    {
                        Color enumColor = GetPlatfromColor(_constructor.LinesX[i].lineY[j]);
                        Rect rect = GUILayoutUtility.GetRect(20, 20);
                        EditorGUI.DrawRect(rect, enumColor);
                        GUI.color = Color.white;

                        _constructor.LinesX[i].lineY[j] = (PlatformType)EditorGUILayout.EnumPopup(_constructor.LinesX[i].lineY[j]);
                        GUI.color = Color.white;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }

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

                case PlatformType.Rock:
                    return Color.red;

                default:
                    return Color.white;
            }
        }
    }
}