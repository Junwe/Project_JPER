using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DummyStageDataSetter : MonoBehaviour
{
    private void Start()
    {
        var temp = ScriptableObject.CreateInstance<StageLevel>();
        temp.LimitX = 1000f;
        StageManager.SelectStage = temp;
    }
}
