using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageLevel SelectStage;
    public StageLevel[] stageLevels;
    public GameObject prefabStageUI;
    public Transform trStageParent;

    GridScroll _gridScroll;

    private List<StageItem> stageItemList = new List<StageItem>();

    // Start is called before the first frame update
    void Start()
    {
        _gridScroll = GetComponent<GridScroll>();
        for (int i = 0; i < (stageLevels.Length / 5) + 1; ++i)
        {
            GameObject stageObject = Instantiate(prefabStageUI);
            StageScrollItem stage = stageObject.GetComponentInChildren<StageScrollItem>();
            stageObject.transform.parent = trStageParent;
            stageObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            _gridScroll.AddStageRectTransform(stage.transform);
            int index = 0;
            for (int j = i * 5; j < (i * 5) + 5; ++j)
            {
                if (stageLevels.Length > j)
                {
                    stage.SetStageItem(index, stageLevels[j], j + 1);
                    index++;
                }
            }

            stage.SetStageText(i * 5 + 1, ((i * 5) + index));
            stageItemList.AddRange(stage.myStageList);
        }

#if UNITY_EDITOR

#else
        SetClearStage();
#endif
    }

    private void SetClearStage()
    {
        for (int i = 0; i < stageItemList.Count; ++i)
        {
            stageItemList[i].SetStageButton(false);
        }

        for (int i = 0; i < stageLevels.Length; ++i)
        {
            if (stageLevels[i].IsClear())
            {
                stageItemList[i].SetStageButton(true);
            }
            else
            {
                stageItemList[i].SetStageButton(true);
                break;
            }
        }
    }
}
