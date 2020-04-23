using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.IO;
using System.Diagnostics;

public class Builder : Editor
{
    [MenuItem("Bulid/BuildWinodw")]
    static void ShowBuildWindow()
    {
        BuildWindow.CreateWindow();
    }

    [MenuItem("Bulid/ClearData")]
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
