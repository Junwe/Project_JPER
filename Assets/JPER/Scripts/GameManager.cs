using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerActionCounter PlayerActionCounter;
    void Start()
    {
        Instantiate(StageManager.SelectStage.stagePrefab);
        Instantiate(StageManager.SelectStage.stageBackGround);

        StartCoroutine(PlayTimer());
    }

    public void OnAdsPopUp()
    {
        PopUpManager.Instance.EnablePopUp("P_Ads");
    }

    public void DisableAdsPopUp()
    {
        PopUpManager.Instance.DisablePopUp("P_Ads");
    }

        public void StopPlayTimer()
    {
        StopCoroutine(PlayTimer());
    }
    private IEnumerator PlayTimer()
    {
        while (true)
        {
            PlayerActionCounter.AddPlayTime(Time.deltaTime);
            yield return null;
        }
    }
}
