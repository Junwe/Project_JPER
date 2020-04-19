using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.IO;
using System.Diagnostics;


[CreateAssetMenu(fileName = "new buildinfo", menuName = "Bulid Infomation", order = 2)]
public class BulidInfomation : ScriptableObject
{
    
    [SerializeField]
    string path;
    [SerializeField]
    string ver;
    [SerializeField]
    string verCode;
    [SerializeField]
    string pass;
    [SerializeField]
    string appName;
    [SerializeField]
    string batFilePath;
    [SerializeField]
    string downloadLink;

    public string Ver => ver;
    public string VerCode => verCode;
    public string Pass => pass;
    public string AppName => appName;
    public string Path => path;
    public string BatFilePath => batFilePath;
    public string DownloadLink => downloadLink;

    string ExecuteProcessTerminal(string argument)
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

    string AddString(string value)
    {
        return "\"" + value + "\"";
    }
    void SendMessageToDiscore(string fileName, string downloadLink, string version, string message)
    {
        ProcessStartInfo processinfo = new ProcessStartInfo();
        Process pro = new Process();
        string Arguments = BatFilePath + " " + AddString(fileName) + " " + AddString(downloadLink)
        + " " + AddString(version) + " " + AddString(message);

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
    public void AndroidBuild(string message)
    {
        int fileCnt = 1;
        string[] scenes = FindEnabledEditorScenes();

        PlayerSettings.Android.keyaliasPass = Pass;
        PlayerSettings.Android.keystorePass = Pass;
        PlayerSettings.Android.bundleVersionCode = int.Parse(VerCode);
        PlayerSettings.bundleVersion = Ver;

        string path = Path;//Application.persistentDataPath;//EditorUtility.OpenFolderPanel("저장 위치를 선택해주세요", "", "");

        path += "/build";

        Directory.CreateDirectory(path);
        string fileName = AppName + GetCurrentDate() + "_" + Ver + "_" + fileCnt + ".apk";

        FileInfo fileInfo = new FileInfo(path + "/" + fileName);

        while (fileInfo.Exists)
        {
            fileCnt++;
            fileName = AppName + GetCurrentDate() + "_" + Ver + "_" + fileCnt + ".apk";
            fileInfo = new FileInfo(path + "/" + fileName);
        }

        BuildPipeline.BuildPlayer(scenes, path + "/" + fileName, BuildTarget.Android, BuildOptions.None);
        System.Diagnostics.Process.Start(path);

        byte[] b = System.Text.Encoding.Default.GetBytes(message);
        string encodiedString = System.Text.Encoding.Unicode.GetString(b);

        SendMessageToDiscore(fileName, path + "/" + fileName, Ver, /*message*/encodiedString);
    }
    public string GetCurrentDate()
    {
        string datestring = "(";
        datestring += System.DateTime.Now.ToString("yyMMdd");
        datestring += ")";
        UnityEngine.Debug.Log(datestring);

        return datestring;
    }

    private string[] FindEnabledEditorScenes()
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
