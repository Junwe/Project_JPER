using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageItem : MonoBehaviour
{
    public Button btnStage;
    public Text txtStage;

    private int _stageSceneCode;
    private StageLevel _myLevel;
    public void SetStageInfomation(StageLevel level,int stageindex)
    {
        txtStage.text = stageindex.ToString();
        _stageSceneCode = level.StageCode;
        _myLevel = level;
    }

    public void SetStageButton(bool onoff)
    {
        btnStage.interactable = onoff;
    }

    public void ClickStageBtn()
    {
        StageManager.SelectStage = _myLevel;
        SceneFade.Instance.SceneLoad(_stageSceneCode);
    }
}
