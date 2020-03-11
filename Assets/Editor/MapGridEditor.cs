using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class MapGridEditor : Editor
{
    Grid grid;

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

        if (Event.current.type == EventType.MouseDown)
        {
            GUIUtility.hotControl = crtID;
            e.Use();
            grid = (Grid)target;
            Vector3 createPos = new Vector3(Mathf.Floor(ray.origin.x / grid.width) * grid.width + grid.width / 2f,
             Mathf.Floor(ray.origin.y / grid.height) * grid.height + grid.height / 2f, 0f);
            if (CheckCompareObject(createPos))
            {
                GameObject createObject = (GameObject)PrefabUtility.InstantiatePrefab(grid.prefabsList[_selectIndex]);
                createObject.transform.parent = grid.transform;
                createObject.transform.position = createPos;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        grid = (Grid)target;
        EditorGUILayout.BeginHorizontal();
        string[] options = new string[grid.prefabsList.Length];

        for (int i = 0; i < options.Length; ++i)
        {
            if (grid.prefabsList[i] != null)
                options[i] = grid.prefabsList[i].name;
        }
        _selectIndex = EditorGUILayout.Popup(_selectIndex, options);

    }

    bool CheckCompareObject(Vector3 newPos)
    {
        Transform[] girdobjList = grid.GetComponentsInChildren<Transform>();
        foreach (var obj in girdobjList)
        {
            if (obj.transform.position == newPos)
            {
                DestroyImmediate(obj.gameObject);
                return false;
            }
        }
        return true;
    }
}
