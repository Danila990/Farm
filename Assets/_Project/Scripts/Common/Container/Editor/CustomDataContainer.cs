using MyCode;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ContainerInfo))]
public class CustomDataContainer : PropertyDrawer
{
    private float spacing = 4f;

    private float objectLabelWidth = 45f;
    private float cacheLabelWidth = 45f;

    private float objectFieldWidth = 100f;
    private float cacheFieldWidth = 40f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect objectLabelRect = new Rect(position.x, position.y, objectLabelWidth, position.height);
        Rect objectFieldRect = new Rect(position.x + objectLabelWidth, position.y, objectFieldWidth, position.height);

        Rect cacheLabelRect = new Rect(objectFieldRect.xMax + spacing, position.y, cacheLabelWidth, position.height);
        Rect cacheFieldRect = new Rect(cacheLabelRect.xMax, position.y, cacheFieldWidth, position.height);

        GUI.Label(objectLabelRect, "Object");
        GUI.Label(cacheLabelRect, "Cache");

        EditorGUI.PropertyField(objectFieldRect, property.FindPropertyRelative("Object"), GUIContent.none);
        EditorGUI.PropertyField(cacheFieldRect, property.FindPropertyRelative("IsCache"), GUIContent.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
