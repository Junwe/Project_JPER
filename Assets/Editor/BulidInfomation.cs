using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

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
    string batFildPath;
    [SerializeField]
    string downloadLink;

    public string Ver => ver;
    public string VerCode => verCode;
    public string Pass => pass;
    public string AppName => appName;
    public string Path => path;
    public string BatFildPath => batFildPath;
    public string DownloadLink => downloadLink;
}
