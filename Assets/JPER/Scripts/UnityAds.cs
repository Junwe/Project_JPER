using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class UnityAds : MonoBehaviour
{
    public PlayerRewind Rewind;
    void Awake()
    {
        Advertisement.Initialize("3526473", false);
    }
    public void ShowRewardedAD()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Rewind.StartRewind();
                Debug.Log("Finished");
                break;
            case ShowResult.Skipped:
                Rewind.StartRewind();
                Debug.Log("Skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Failed");
                break;
        }
    }
}
