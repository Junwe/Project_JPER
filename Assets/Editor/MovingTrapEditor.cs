using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MovingTrap))]
public class MovingTrapEditor : Editor
{
    MovingTrap targetObject;
    void OnSceneGUI()
    {
        targetObject = (MovingTrap)target;

        for (int i = 0; i < targetObject.points.Count; i++)
        {
            targetObject.points[i] = Handles.PositionHandle(targetObject.points[i], Quaternion.identity);
        }

        Handles.DrawAAPolyLine(targetObject.points.ToArray());

        Handles.BeginGUI();
        if (GUILayout.Button("포인트 추가", GUILayout.Width(80f)))
        {
            Undo.RecordObject(targetObject, "add points");
            targetObject.points.Add(targetObject.transform.position);
        }
        if (GUILayout.Button("포인트 없애기", GUILayout.Width(80f)))
        {
            if (targetObject.points.Count > 0)
            {
                Undo.RecordObject(targetObject, "remove points");
                targetObject.points.RemoveAt(targetObject.points.Count - 1);
            }
        }
        Handles.EndGUI();
    }
}
