using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScrollItem : MonoBehaviour
{
    public StageItem[] myStageList;
    void Start()
    {
    }

    public void SetStageItem(int index, StageLevel level, int stageindex)
    {
        if (myStageList.Length == 0)
        {
            myStageList = GetComponentsInChildren<StageItem>();
            for (int i = 0; i < myStageList.Length; ++i)
            {
                myStageList[i].gameObject.SetActive(false);
            }
        }

        if (myStageList.Length > index && myStageList[index] != null)
        {
            myStageList[index].gameObject.SetActive(true);
            myStageList[index].SetStageItem(level, stageindex);
        }
    }
}
