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
    public void SetStageItem(StageLevel level)
    {
        txtStage.text = level.stageName;
        _stageSceneCode = level.StageCode;
    }

    public void ClickStageBtn()
    {
        SceneManager.LoadScene(_stageSceneCode);
    }
}
