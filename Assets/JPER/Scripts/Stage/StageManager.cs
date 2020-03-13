using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageLevel SelectStage;
    public StageLevel[] stageLevels;
    public GameObject prefabStageUI;
    public Transform trStageParent;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stageLevels.Length; ++i)
        {
            StageItem stage = Instantiate(prefabStageUI).GetComponent<StageItem>();
            stage.transform.parent = trStageParent;
            stage.SetStageItem(stageLevels[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
