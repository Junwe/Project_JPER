﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoDestorySingleton<GameManager>
{
    public PlayerMove PlayerMove;
    public PlayerActionCounter playerActionCounter = new PlayerActionCounter();
    [SerializeField]
    private TutorialManager _tutorialManager;
    void Awake()
    {
        if (StageManager.SelectStage.stagePrefab != null)
        {
            Transform stage = Instantiate(StageManager.SelectStage.stagePrefab).GetComponent<Transform>();
            Instantiate(StageManager.SelectStage.stageBackGround);

            Transform[] childs = stage.GetComponentsInChildren<Transform>();

            foreach (var obj in childs)
            {
                if (obj.name.Contains("G_Ground"))
                {
                    if (obj.transform.position.y + 0.35f <= PlayerMove.LimitY)
                    {
                        PlayerMove.LimitY = obj.transform.position.y + 0.35f;
                    }
                }
                else
                    continue;
            }
        }
        StartCoroutine(PlayTimer());

        if (PlayerPrefs.GetInt("tutorial", 0) == 0)
        {
            _tutorialManager.StartTutorial();
            PlayerPrefs.SetInt("tutorial", 1);
        }
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
