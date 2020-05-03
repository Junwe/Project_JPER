using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopUp_Option : MonoBehaviour
{
    public Text txtEffSound;
    public Text txtBGMSound;

    public GameObject objMainBtn;

    void Start()
    {
        SetOptionPopUp();
        SceneManager.sceneLoaded += SetOptionPopUp;
    }
    public void ClickMain()
    {
        ClickExit();
       SceneFade.Instance.SceneLoad(0);
        // GolobalUI.Instance.SceneLoad(0);
        SceneManager.sceneLoaded += SetOptionPopUp;
    }

    public void ClickQuit()
    {
        Application.Quit();
    }

    public void ActiveMainBtn(bool active)
    {
        objMainBtn.SetActive(active);
    }

    public void SetEnablePopUp()
    {
        if(PlayerPrefs.GetInt("SoundEff", 1) == 0)
        {
            txtEffSound.text = "Off";
        }
        else
        {
            txtEffSound.text = "On";
        }
        
        if(PlayerPrefs.GetInt("SoundBgm", 1) == 0)
        {
            txtBGMSound.text = "Off";
        }
        else
        {
            txtBGMSound.text = "On";
        }

        Time.timeScale = 0f;
    }

    public void SetDisablePopUp()
    {
        Time.timeScale = 1f;
    }

    public void ClickEffSound()
    {
        if(PlayerPrefs.GetInt("SoundEff", 1) == 0)
        {
            Sound.Instance.SetMute(1, "SoundEff");
            txtEffSound.text = "On";
        }
        else
        {
            Sound.Instance.SetMute(0, "SoundEff");
            txtEffSound.text = "Off";
        }
    }

    public void ClickBGMSound()
    {
        if(PlayerPrefs.GetInt("SoundBgm", 1) == 0)
        {
            Sound.Instance.SetMute(1, "SoundBgm");
            txtBGMSound.text = "On";
        }
        else
        {
            Sound.Instance.SetMute(0, "SoundBgm");
            txtBGMSound.text = "Off";
        }
    }
    public void ClickExit()
    {
        PopUpManager.Instance.DisablePopUp("P_Option");
    }

    public void ClickOptinBtn()
    {
        PopUpManager.Instance.EnablePopUp("P_Option", "SetEnablePopUp", "SetDisablePopUp");
    }

    private void SetOptionPopUp(Scene scene, LoadSceneMode mode)
    {
        // if(SceneManager.GetActiveScene().buildIndex == 0)
        // {
        //     objMainBtn.SetActive(false);
        // }
        // else
        // {
        //     objMainBtn.SetActive(true);
        // }
    }

    private void SetOptionPopUp()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            objMainBtn.SetActive(false);
        }
        else
        {
            objMainBtn.SetActive(true);
        }
    }
}
