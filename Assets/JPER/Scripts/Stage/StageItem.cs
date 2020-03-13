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
    public void SetStageItem(StageLevel level)
    {
        txtStage.text = level.stageName;
        _stageSceneCode = level.StageCode;
        _myLevel = level;
    }

    public void ClickStageBtn()
    {
        StageManager.SelectStage = _myLevel;
        SceneManager.LoadScene(_stageSceneCode);
    }
}
