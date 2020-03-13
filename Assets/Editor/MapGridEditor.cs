﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class MapGridEditor : Editor
{
    Grid grid;

    private int _objectIndex = 0;
    private int _selectIndex = 0;

    void OnEanble()
    {
        grid = (Grid)target;
    }
    void OnSceneGUI()
    {
        int crtID = GUIUtility.GetControlID(FocusType.Passive);
        Event e = Event.current;

        var mousePosition = Event.current.mousePosition * EditorGUIUtility.pixelsPerPoint;
        mousePosition.y = Camera.current.pixelHeight - mousePosition.y;
        Ray ray = Camera.current.ScreenPointToRay(mousePosition);
        
        if (_selectIndex == 0)
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
        else if (_selectIndex == 1)
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

    private void DrawObject(bool drag, Vector3 pos)
    {
        grid = (Grid)target;
        Vector3 createPos = new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
         Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, 0f);
        if (CheckCompareObject(createPos, drag))
        {
            GameObject createObject = (GameObject)PrefabUtility.InstantiatePrefab(grid.prefabsList[_objectIndex]);
            createObject.transform.parent = grid.parent;
            createObject.transform.position = createPos;
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
        _objectIndex = EditorGUILayout.Popup(_objectIndex, ObjectOptions);
        _selectIndex = EditorGUILayout.Popup(_selectIndex, selectOptions);

        EditorGUILayout.EndHorizontal();

    }

    bool CheckCompareObject(Vector3 newPos, bool destoryObject)
    {
        Transform[] girdobjList = grid.parent.GetComponentsInChildren<Transform>();
        foreach (var obj in girdobjList)
        {
            if (obj.transform.position == newPos)
            {
                if (destoryObject)
                    DestroyImmediate(obj.gameObject);
                return false;
            }
        }
        return true;
    }
}