using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Instantiate(StageManager.SelectStage.stagePrefab);
        Instantiate(StageManager.SelectStage.stageBackGround);
    }

    public void OnAdsPopUp()
    {
        PopUpManager.Instance.EnablePopUp("P_Ads");
    }

    public void DisableAdsPopUp()
    {
        PopUpManager.Instance.DisablePopUp("P_Ads");
    }
}
