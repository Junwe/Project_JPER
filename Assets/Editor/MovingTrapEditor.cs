using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MovingTrap))]
public class MovingTrapEditor : Editor
{
    MovingTrap targetObject;
    Grid grid;

    bool _isGridMove = false;
    void OnSceneGUI()
    {
        targetObject = (MovingTrap)target;
        grid = GameObject.Find("MapCreator").GetComponent<Grid>();

        if (_isGridMove == false)
        {
            for (int i = 0; i < targetObject.points.Count; i++)
            {
                targetObject.points[i] = Handles.PositionHandle(targetObject.points[i], Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < targetObject.points.Count; i++)
            {
                targetObject.points[i] = Handles.PositionHandle(targetObject.points[i], Quaternion.identity);

                targetObject.points[i] = GetGirdPosition(targetObject.points[i]);
            }
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

        GUILayout.BeginHorizontal();
        GUILayout.Box("그리그 모드");
        _isGridMove = EditorGUILayout.Toggle(_isGridMove);
        
        GUILayout.EndHorizontal();
        Handles.EndGUI();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        grid = (Grid)EditorGUILayout.ObjectField(grid, typeof(Grid));
    }

    private Vector3 GetGirdPosition(Vector3 pos)
    {
        return new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
         Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, 0f);
    }
}
