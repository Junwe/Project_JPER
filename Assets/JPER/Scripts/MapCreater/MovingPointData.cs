using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

[System.Serializable]
public class MovingPointData
{
    public Vector3 point;
    public bool disableCollision;
}

[CustomPropertyDrawer(typeof(MovingPointData))]
public class MovingPointDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float vectorWidth = 300;
        float fieldSpace = 10;

        // Calculate rects
        var pointRect = new Rect(position.x, position.y, vectorWidth, position.height);
        var disableCollisionRect = new Rect(position.x + vectorWidth + fieldSpace, position.y, 50, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(pointRect, property.FindPropertyRelative("point"), GUIContent.none);
        EditorGUI.PropertyField(disableCollisionRect, property.FindPropertyRelative("disableCollision"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
