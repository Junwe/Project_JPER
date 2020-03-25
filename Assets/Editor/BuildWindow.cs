using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BuildWindow : EditorWindow
{
    string _buildMessge;
    [SerializeField]
    BulidInfomation _buildInfo;
    public static void CreateWindow()
    {
        BuildWindow window = (BuildWindow)GetWindow(typeof(BuildWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Build Window", EditorStyles.boldLabel);
        _buildInfo = (BulidInfomation)EditorGUILayout.ObjectField("info", _buildInfo, typeof(BulidInfomation));
        _buildMessge = EditorGUILayout.TextField("BuildMessage", _buildMessge, GUILayout.Height(100f));
        if (GUILayout.Button("안드로이드 빌드!"))
        {
            _buildInfo.AndroidBuild(_buildMessge);
        }
        GUILayout.EndVertical();
        if (_buildInfo != null)
            EditorUtility.SetDirty(_buildInfo);
    }
}
