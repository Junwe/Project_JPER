using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.IO;
using System.Diagnostics;

public class Builder : EditorWindow
{
    static string ExecuteProcessTerminal(string argument)
    {
        try
        {
            UnityEngine.Debug.Log("============== Start Executing [" + argument + "] ===============");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = false,
                Arguments = " -c \"" + argument + " \""
            };
            Process myProcess = new Process
            {
                StartInfo = startInfo
            };
            myProcess.Start();
            string output = myProcess.StandardOutput.ReadToEnd();
            UnityEngine.Debug.Log(argument);
            myProcess.WaitForExit();
            UnityEngine.Debug.Log("============== End ===============");
 
            return output;
        }
        catch (System.Exception e)
        {
            return null;
        }
    }

    static string AddString(string value)
    {
        return "\""+ value +"\"";
    }
    static void SendMessageToDiscore(string fileName,string downloadLink,string version)
    {
        BulidInfomation buildinfo = AssetDatabase.LoadAssetAtPath<BulidInfomation>("Assets/Editor/AndroidInfomation.asset");
        ProcessStartInfo processinfo = new ProcessStartInfo();
        Process pro = new Process();
        string Arguments = buildinfo.BatFildPath + " " + AddString(fileName) + " " + AddString(downloadLink) + " " + AddString(version);

#if UNITY_EDITOR_WIN
        processinfo.FileName = "cmd.exe";
        processinfo.RedirectStandardInput = true;
        processinfo.UseShellExecute = false;
        pro.StartInfo = processinfo;
        pro.Start();
        pro.StandardInput.Write(Arguments + System.Environment.NewLine);
        pro.StandardInput.Close();
#elif UNITY_EDITOR_OSX
        ExecuteProcessTerminal(Arguments);
#endif
    }
    [MenuItem("Bulid/Android")]
    static void AndroidBuild()
    {
        int fileCnt = 1;
        string[] scenes = FindEnabledEditorScenes();

        BulidInfomation info = AssetDatabase.LoadAssetAtPath<BulidInfomation>("Assets/Editor/AndroidInfomation.asset");
        PlayerSettings.Android.keyaliasPass = info.Pass;
        PlayerSettings.Android.keystorePass = info.Pass;
        PlayerSettings.Android.bundleVersionCode = int.Parse(info.VerCode);
        PlayerSettings.bundleVersion = info.Ver;

        string path = info.Path;//Application.persistentDataPath;//EditorUtility.OpenFolderPanel("저장 위치를 선택해주세요", "", "");

        path += "/build";

        Directory.CreateDirectory(path);
        string fileName = info.AppName + GetCurrentDate() + "_" + info.Ver + "_" + fileCnt + ".apk";

        FileInfo fileInfo = new FileInfo(path + "/" + fileName);

        while (fileInfo.Exists)
        {
            fileCnt++;
            fileName = info.AppName + GetCurrentDate() + "_" + info.Ver + "_" + fileCnt + ".apk";
            fileInfo = new FileInfo(path + "/" + fileName);
        }

        BuildPipeline.BuildPlayer(scenes, path + "/" + fileName, BuildTarget.Android, BuildOptions.None);
        System.Diagnostics.Process.Start(path);

        SendMessageToDiscore(fileName,path + "/" + fileName,info.Ver);
    }
    public static string GetCurrentDate()
    {
        string datestring = "(";
        datestring += System.DateTime.Now.ToString("yyMMdd");
        datestring += ")";
        UnityEngine.Debug.Log(datestring);

        return datestring;
    }


    [MenuItem("Bulid/ClearData")]
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

    static private string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }
}
