using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DummyStageDataSetter : MonoBehaviour
{
    private void Start()
    {
        StageManager.SelectStage = new StageLevel()
        {
            LimitX = 100
        };
    }
}
