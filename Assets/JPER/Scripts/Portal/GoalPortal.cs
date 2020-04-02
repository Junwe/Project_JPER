using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPortal : AbstPortal
{
    public override void UsePortal(PortalExecuter calledExecuter)
    {
        StageManager.SelectStage.SetStageClear();
        GameManager.Instance.OnResultPopUp();
        // SceneFade.Instance.SceneLoad(0);
    }
}
