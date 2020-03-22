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

    // Start is called before the first frame update
    void Start()
    {
        _gridScroll = GetComponent<GridScroll>();
        for (int i = 0; i < (stageLevels.Length / 5); ++i)
        {
            StageScrollItem stage = Instantiate(prefabStageUI).GetComponent<StageScrollItem>();
            stage.transform.parent = trStageParent;
            _gridScroll.AddStageRectTransform(stage.GetComponent<RectTransform>());
            int index = 0;
            for (int j = i * 5; j < (i * 5) + 5; ++j)
            {
                if (stageLevels.Length > j)
                {
                    stage.SetStageItem(index, stageLevels[j], j + 1);
                    index++;
                }
            }
        }
    }
}
