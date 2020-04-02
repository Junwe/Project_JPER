using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoDestorySingleton<GameManager>
{
    public PlayerActionCounter playerActionCounter = new PlayerActionCounter();

    void Start()
    {
        Instantiate(StageManager.SelectStage.stagePrefab);
        Instantiate(StageManager.SelectStage.stageBackGround);

        StartCoroutine(PlayTimer());
    }

    public void OnResultPopUp()
    {
        PopUpManager.Instance.EnablePopUp("P_GameResult","PlayReulst");
    }

    public void OnAdsPopUp()
    {
        PopUpManager.Instance.EnablePopUp("P_Ads");
    }

    public void DisableAdsPopUp()
    {
        PopUpManager.Instance.DisablePopUp("P_Ads");
    }

    public void AddJumpCount()
    {
        playerActionCounter.AddJumpCount();
    }

    public void AddFallCount()
    {
        playerActionCounter.AddFallCount();
    }

    public void StopPlayTimer()
    {
        StopCoroutine(PlayTimer());
    }

    private IEnumerator PlayTimer()
    {
        while (true)
        {
            playerActionCounter.AddPlayTime(Time.deltaTime);
            yield return null;
        }
    }

    public void GoToMainScene()
    {
        StageManager.SelectStage.SetStageClear();
        SceneFade.Instance.SceneLoad(0);
    }
}
