using System;
using UnityEditor;
using UnityEngine;

namespace MyCode 
{
    [CustomEditor(typeof(GridMapSetting))]
    public class CustomGridInfoArrayEditor : Editor
    {
        private GridMapSetting _gridInfo;
        private PlatformLineInfo[] _platformLineInfos;
        private Vector2Int _vectorSize;

        public override void OnInspectorGUI()
        {
            _gridInfo = (GridMapSetting)target;
            base.OnInspectorGUI();
            _platformLineInfos = _gridInfo.GetLines();
            LabelFieldText();

            InitGrid();
            CustomArray();
            EditorGUILayout.Space(5);
            LabelFieldTextSizeGrid();

            EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            AddLineButton();
            RemoveLineButton();
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            AddRowButton();
            RemoveRowButton();
            EditorGUILayout.EndHorizontal();
            ResetButton();
            EditorGUILayout.EndHorizontal();

            _gridInfo.SetLines(_platformLineInfos);
            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }

        private void CustomArray()
        {
            for (int i = 0; i < _platformLineInfos.Length; i++)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                if (_platformLineInfos[i] != null)
                {
                    for (int j = 0; j < _platformLineInfos[i].platformTypes.Length; j++)
                    {
                        Color enumColor = GetPlatfromColor(_platformLineInfos[i].platformTypes[j]);
                        Rect rect = GUILayoutUtility.GetRect(20, 20); 
                        EditorGUI.DrawRect(rect, enumColor);
                        GUI.color = Color.white;

                        _platformLineInfos[i].platformTypes[j] = (PlatformType)EditorGUILayout.EnumPopup(_platformLineInfos[i].platformTypes[j]);
                        GUI.color = Color.white;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private Color GetPlatfromColor(PlatformType platformType)
        {
            Color color;
            switch (platformType)
            {
                case PlatformType.Default:
                    color = Color.white;
                    break;

                case PlatformType.Spawn:
                    color = Color.green;
                    break;

                case PlatformType.Empty:
                    color = Color.black;
                    break;

                default:
                    color = Color.white;
                    break;
            }

            return color;
        }

        private void LabelFieldTextSizeGrid()
        {
            Vector2Int sizeGrid = new Vector2Int(_platformLineInfos.Length, _platformLineInfos[0].platformTypes.Length);
            EditorGUILayout.LabelField($"Size GameGrid: X - {sizeGrid.x}, Y - {sizeGrid.y}");
        }

        private void AddLineButton()
        {
            if (GUILayout.Button("Add Line"))
            {
                PlatformLineInfo[] newGridLines = new PlatformLineInfo[_platformLineInfos.Length + 1];
                _platformLineInfos.CopyTo(newGridLines, 0);
                newGridLines[_platformLineInfos.Length] = new PlatformLineInfo() { platformTypes = new PlatformType[_platformLineInfos[0].platformTypes.Length] };
                _platformLineInfos = newGridLines;
            }
        }

        private void RemoveLineButton()
        {
            if (GUILayout.Button("Remove Line"))
            {
                if (_platformLineInfos.Length <= 1)
                    return;

                PlatformLineInfo[] newGridLines = new PlatformLineInfo[_platformLineInfos.Length - 1];
                Array.Copy(_platformLineInfos, newGridLines, newGridLines.Length);
                _platformLineInfos = newGridLines;
            }
        }

        private void RemoveRowButton()
        {
            if (GUILayout.Button("Remove Row"))
            {
                if(_platformLineInfos[0].platformTypes.Length <= 1)
                    return;

                for (int i = 0; i < _platformLineInfos.Length; i++)
                {
                    PlatformType[] newRow = new PlatformType[_platformLineInfos[i].platformTypes.Length -1];
                    Array.Copy(_platformLineInfos[i].platformTypes, newRow, newRow.Length);
                    _platformLineInfos[i].platformTypes = newRow;
                }
            }
        }

        private void AddRowButton()
        {
            if (GUILayout.Button("Add Row"))
            {
                for (int i = 0; i < _platformLineInfos.Length; i++)
                {
                    PlatformType[] newRow = new PlatformType[_platformLineInfos[i].platformTypes.Length + 1];
                    _platformLineInfos[i].platformTypes.CopyTo(newRow, 0);
                    _platformLineInfos[i].platformTypes = newRow;
                }
            }
        }

        private void ResetButton()
        {
            if (GUILayout.Button("Reset"))
            {
                _platformLineInfos = new PlatformLineInfo[1] { new PlatformLineInfo() { platformTypes = new PlatformType[1] { PlatformType.Default } } };
            }
        }

        private void LabelFieldText()
        {
            EditorGUILayout.Space(10);
            GUIStyle centeredStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter

            };

            EditorGUILayout.LabelField("Platform Grid", centeredStyle);
        }

        private void InitGrid()
        {
            if (_platformLineInfos == null || _platformLineInfos.Length == 0)
            {
                _platformLineInfos = new PlatformLineInfo[1]
                {
                    new PlatformLineInfo()
                    {
                        platformTypes = new PlatformType[1]{ PlatformType.Default }
                    }
                };
            }
        }
    }
}