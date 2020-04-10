using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoDestorySingleton<GameManager>
{
    public PlayerActionCounter playerActionCounter = new PlayerActionCounter();

    void Start()
    {
        if (StageManager.SelectStage.stagePrefab != null)
        {
            Instantiate(StageManager.SelectStage.stagePrefab);
            Instantiate(StageManager.SelectStage.stageBackGround);
        }
        StartCoroutine(PlayTimer());
    }

    public void OnResultPopUp()
    {
        PopUpManager.Instance.EnablePopUp("P_GameResult", "PlayReulst", " ", false);
        StopPlayTimer();
        Sound.Instance.PlayEffSound(SOUND.S_CLEAR);
    }

    public void OnAdsPopUp()
    {
        PopUpManager.Instance.EnablePopUp("P_Ads", " ", " ", false);
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

    public void AddHitCount()
    {
        playerActionCounter.AddHitCount();
    }

    public void AddMoveDistacne(float distance)
    {
        playerActionCounter.AddMoveDisctcne(distance);
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
