using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageScrollItem : MonoBehaviour
{
    public StageItem[] myStageList;
    public Text txtStageName;
    void Awake()
    {
        myStageList = GetComponentsInChildren<StageItem>();
        for (int i = 0; i < myStageList.Length; ++i)
        {
            myStageList[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        txtStageName.transform.localPosition = transform.localPosition + new Vector3(13f, 300f, 0f);
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
            myStageList[index].SetStageInfomation(level, stageindex);
        }
    }
    public void SetStageText(int minimum,int maximum)
    {
        txtStageName.text = "STAGE " + (minimum).ToString() + " - " + (maximum).ToString();

    }
}
