using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapObject)), CanEditMultipleObjects]
public class MapObjectEditor : Editor
{
    MapObject targetObject;
    Grid grid;

    void OnSceneGUI()
    {
        targetObject = (MapObject)target;
        grid = GameObject.Find("MapCreator").GetComponent<Grid>();

        targetObject.transform.position = GetGirdPosition(targetObject.transform.position);
    }
    private Vector3 GetGirdPosition(Vector3 pos)
    {
        return new Vector3(Mathf.Floor(pos.x / grid.width) * grid.width + grid.width / 2f,
         Mathf.Floor(pos.y / grid.height) * grid.height + grid.height / 2f, 0f);
    }
}
