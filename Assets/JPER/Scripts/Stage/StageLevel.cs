using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new stageLevel", menuName = "Stage Level", order = 2)]
public class StageLevel : ScriptableObject
{
    public GameObject stagePrefab;
    public GameObject stageBackGround;
    public string stageName;
    public int StageCode = 1;
    public float LimitMinX = -100f;
    public float LimitMaxX = 100f;


    public bool IsClear()
    {
        return PlayerPrefs.GetInt(stageName, 0) == 0 ? false : true;
    }

    public void SetStageClear()
    {
        PlayerPrefs.SetInt(stageName, 1);
    }
}
