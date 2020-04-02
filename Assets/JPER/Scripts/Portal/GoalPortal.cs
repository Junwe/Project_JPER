using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPortal : AbstPortal
{
    public override void UsePortal(PortalExecuter calledExecuter)
    {
        StageManager.SelectStage.SetStageClear();
        SceneFade.Instance.SceneLoad(0);

        // TODO
        // gameManager 통해 게임 중지 및 결과 UI 출력.
        GameManager.Instance.OnResultPopUp();
    }

    private void Start()
    {
        // SceneFade.Instance.SceneLoad(0);
    }
}
