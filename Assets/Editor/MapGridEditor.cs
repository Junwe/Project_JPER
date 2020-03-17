using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class MapGridEditor : Editor
{
    Grid grid;
    void OnEanble()
    {
        grid = (Grid)target;
    }
    void OnSceneGUI()
    {
        grid = (Grid)target;

        int crtID = GUIUtility.GetControlID(FocusType.Passive);
        Event e = Event.current;

        var mousePosition = Event.current.mousePosition * EditorGUIUtility.pixelsPerPoint;
        mousePosition.y = Camera.current.pixelHeight - mousePosition.y;
        Ray ray = Camera.current.ScreenPointToRay(mousePosition);

        Handles.BeginGUI();
        DrawPainter();
        Handles.EndGUI();

        if (Event.current.button == 0)
        {
            if (grid.SelectIndex == 0)
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    GUIUtility.hotControl = crtID;
                    e.Use();
                    DrawObject(true, ray.origin);
                }
                if (Event.current.type == EventType.MouseDrag)
                {
                    DrawObject(false, ray.origin);
                }
            }
            else if (grid.SelectIndex == 1)
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    GUIUtility.hotControl = crtID;
                    e.Use();
                    DestoryObject(ray.origin);
                }
                if (Event.current.type == EventType.MouseDrag)
                {
                    DestoryObject(ray.origin);
                }
            }
        }
    }

    private void DrawObject(bool drag, Vector3 pos)
    {
        grid = (Grid)target;
        Vector3 createPos = new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
         Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, 0f);
        if (CheckCompareObject(createPos, drag))
        {
            GameObject createObject = (GameObject)PrefabUtility.InstantiatePrefab(grid.prefabsList[grid.ObjectIndex]);
            createObject.transform.parent = grid.parent;
            createObject.transform.position = createPos;
            Undo.RegisterCreatedObjectUndo(createObject, "create Object");
        }
    }

    private void DestoryObject(Vector3 pos)
    {
        grid = (Grid)target;
        Vector3 createPos = new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
         Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, 0f);
        if (CheckCompareObject(createPos, true))
        {
        }
    }

    private void DrawPainter()
    {
        grid = (Grid)target;
        string[] ObjectOptions = new string[grid.prefabsList.Length];
        string[] selectOptions = { "그리기", "지우기" };

        for (int i = 0; i < ObjectOptions.Length; ++i)
        {
            if (grid.prefabsList[i] != null)
                ObjectOptions[i] = grid.prefabsList[i].name;
        }
        grid.ObjectIndex = EditorGUILayout.Popup(grid.ObjectIndex, ObjectOptions, GUILayout.Width(80f));
        grid.SelectIndex = EditorGUILayout.Popup(grid.SelectIndex, selectOptions, GUILayout.Width(80f));
        if (GUILayout.Button("모두 삭제", GUILayout.Width(80f)))
        {
            AllObjectDelete();
        }
    }

    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        grid = (Grid)target;
        EditorGUILayout.BeginHorizontal();
        string[] ObjectOptions = new string[grid.prefabsList.Length];
        string[] selectOptions = { "그리기", "지우기" };

        for (int i = 0; i < ObjectOptions.Length; ++i)
        {
            if (grid.prefabsList[i] != null)
                ObjectOptions[i] = grid.prefabsList[i].name;
        }
        grid.ObjectIndex = EditorGUILayout.Popup(grid.ObjectIndex, ObjectOptions);
        grid.SelectIndex = EditorGUILayout.Popup(grid.SelectIndex, selectOptions);

        EditorGUILayout.EndHorizontal();

    }

    void AllObjectDelete()
    {
        Transform[] girdobjList = grid.parent.GetComponentsInChildren<Transform>();
        foreach (var obj in girdobjList)
        {
            if (obj != null && obj != grid.parent)
                Undo.DestroyObjectImmediate(obj.gameObject);
        }
    }

    bool CheckCompareObject(Vector3 newPos, bool destoryObject)
    {
        Transform[] girdobjList = grid.parent.GetComponentsInChildren<Transform>();
        foreach (var obj in girdobjList)
        {
            if (obj.transform.position == newPos)
            {
                if (destoryObject)
                {
                    Undo.DestroyObjectImmediate(obj.gameObject);
                    //DestroyImmediate(obj.gameObject);

                }
                return false;
            }
        }
        return true;
    }
}
