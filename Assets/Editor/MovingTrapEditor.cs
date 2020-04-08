using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(MovingTrap)), CanEditMultipleObjects]
public class MovingTrapEditor : Editor
{
    MovingTrap targetObject;
    Grid grid;

    void OnSceneGUI()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            targetObject = (MovingTrap)target;
            grid = GameObject.Find("MapCreator").GetComponent<Grid>();

            for (int i = 0; i < targetObject.points.Count; i++)
            {
                targetObject.points[i] = GetGirdPosition(targetObject.points[i]);
                targetObject.points[i] = Handles.PositionHandle(targetObject.points[i], Quaternion.identity);
            }

            Handles.DrawAAPolyLine(targetObject.points.ToArray());

            Handles.BeginGUI();
            if (GUILayout.Button("포인트 추가", GUILayout.Width(80f)))
            {
                Undo.RecordObject(targetObject, "add points");
                targetObject.points.Add(new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z - 1f));
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
            //GUILayout.Box("그리드 모드");
            //grid.IsGridMove = EditorGUILayout.Toggle(grid.IsGridMove);

            GUILayout.EndHorizontal();
            Handles.EndGUI();

            EditorUtility.SetDirty(targetObject);
        }

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private Vector3 GetGirdPosition(Vector3 pos)
    {
        return new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
         Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, -1f);
    }
}
