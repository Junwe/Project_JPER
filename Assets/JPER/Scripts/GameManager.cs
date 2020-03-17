using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Instantiate(StageManager.SelectStage.stagePrefab);       
        Instantiate(StageManager.SelectStage.stageBackGround);       
    }
}
