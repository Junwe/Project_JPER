using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(MovingMapObject)), CanEditMultipleObjects]
public class MovingTrapEditor : Editor
{
    MovingMapObject targetObject;
    Grid grid;

    void OnSceneGUI()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            targetObject = (MovingMapObject)target;
            grid = GameObject.Find("MapCreator").GetComponent<Grid>();

            if (targetObject.points != null)
            {
                var polyLinePoints = new List<Vector3>(targetObject.points.Count);

                for (int i = 0; i < targetObject.points.Count; i++)
                {
                    targetObject.points[i].point = GetGirdPosition(targetObject.points[i].point);
                    targetObject.points[i].point = Handles.PositionHandle(targetObject.points[i].point, Quaternion.identity);
                    polyLinePoints.Add(targetObject.points[i].point);
                }

                Handles.DrawAAPolyLine(polyLinePoints.ToArray());
                EditorUtility.SetDirty(targetObject);
            }

            Handles.BeginGUI();
            if (GUILayout.Button("포인트 추가", GUILayout.Width(80f)))
            {
                Undo.RecordObject(targetObject, "add points");
                targetObject.points.Add(new MovingPointData()
                {
                    point = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z - 1f),
                    disableCollision = false
                });
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
        }

    }

    UnityEditorInternal.ReorderableList reorderableList = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (reorderableList == null)
        {
            var property = serializedObject.FindProperty("points");
            reorderableList = new UnityEditorInternal.ReorderableList(serializedObject, property);

            reorderableList.drawElementCallback = (rect, index, isActive, isFocus) =>
            {
                var element = property.GetArrayElementAtIndex(index);
                rect.width -= 20;
                rect.height -= 4;
                rect.y += 2;
                EditorGUI.PropertyField(rect, element);
            };

            reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, property.displayName);
        }

        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private Vector3 GetGirdPosition(Vector3 pos)
    {
        return new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
         Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, -1f);
    }
}
