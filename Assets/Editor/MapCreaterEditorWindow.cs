using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapCreaterEditorWindow : EditorWindow
{
    private static Grid _girdObject;

    public SerializedObject serializedObject;

    float width;
    float height;
    GameObject[] prefabList;
    public static void Open(Grid grid)
    {
        _girdObject = grid;

        MapCreaterEditorWindow window = (MapCreaterEditorWindow)EditorWindow.GetWindow(typeof(MapCreaterEditorWindow));

        window.serializedObject = new SerializedObject(grid);

        window.Show();
    }

    void OnGUI()
    {
        width = EditorGUILayout.FloatField("width", width, GUILayout.Height(150));
        height = EditorGUILayout.FloatField("height", height, GUILayout.Height(150));
    }


}
