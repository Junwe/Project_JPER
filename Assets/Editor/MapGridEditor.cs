using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class MapGridEditor : Editor
{
    Grid grid;

    private List<GameObject> _objList = new List<GameObject>();

    void OnEanble()
    {
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
                GameObject createObject = (GameObject)PrefabUtility.InstantiatePrefab(grid.prefabsList);
                createObject.transform.parent = grid.transform;
                createObject.transform.position = createPos;
                _objList.Add(createObject);
            }
        }
    }

    bool CheckCompareObject(Vector3 newPos)
    {
        foreach (var obj in _objList)
        {
            if (obj.transform.position == newPos)
            {
                _objList.Remove(obj);
                DestroyImmediate(obj);
                return false;
            }
        }
        return true;
    }
}
