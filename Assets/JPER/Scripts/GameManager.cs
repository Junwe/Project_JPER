using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityAds UnityAds;
    void Start()
    {
        Instantiate(StageManager.SelectStage.stagePrefab);       
        Instantiate(StageManager.SelectStage.stageBackGround);       
    }
}
