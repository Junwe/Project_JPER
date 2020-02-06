using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SOUND
{
    S_PISTOL_SHOT = 0,
    S_SHOTGUN_SHOT,
    S_SNIPER_SHOT,
    S_JUMP_HOT,
    S_JUMP_SFX,
    S_BGM,
    S_COIN,
    S_Grenade,
    S_Flamethrower,
    S_BOOMERANG,
    S_BTN,
    S_SHOT_BUBBLE,
    S_POP_BUBBLE,
    S_SHOT_LASER,
    S_SHOT_Revolver,
    
}

public class Sound : MonoSingleton<Sound>
{
    public AudioSource[] effSource;
    public AudioSource bgmSource;
    public AudioClip[] audioClips;

    private Dictionary<SOUND, AudioClip> _soundDictionary = new Dictionary<SOUND, AudioClip>();

    private void Awake()
    {
        for (int i = 0; i < audioClips.Length; ++i)
        {
            _soundDictionary.Add((SOUND)i, audioClips[i]);
        }
        SetMute(PlayerPrefs.GetInt("SoundEff", 1),"SoundEff");
        SetMute(PlayerPrefs.GetInt("SoundBgm", 1),"SoundBgm");

        //PlayBGMSound(SOUND.S_BGM);

        SceneManager.sceneLoaded += offAllEffSound;
    }

    private void Start()
    {
        PlayBGMSound(SOUND.S_BGM);
    }

    void offAllEffSound(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < effSource.Length; ++i)
        {
            if (effSource[i].isPlaying)
            {
                effSource[i].Stop();
                return;
            }
        }
    }
    // 0이면 뮤트, 1이면 활성화
    public void SetMute(int mute,string type)
    {
        bool isMute = mute == 0 ? true : false;
        if (type.Equals("SoundEff"))
        {
            for (int i = 0; i < effSource.Length; ++i)
            {
                effSource[i].mute = isMute;
            }
        }
        else if(type.Equals("SoundBgm"))
        {
            bgmSource.mute = isMute;
        }
        PlayerPrefs.SetInt(type, mute);
    }

    public void PlayEffSound(SOUND idx, float volume = 1f, bool loop = false)
    {
        for (int i = 0; i < effSource.Length; ++i)
        {
            if (loop == false)
            {
                if (!effSource[i].isPlaying)
                {
                    effSource[i].clip = _soundDictionary[idx];
                    effSource[i].volume = volume;
                    effSource[i].Play();
                    return;
                }
            }
            else
            {
                if (!effSource[i].isPlaying && effSource[i].loop)
                {
                    effSource[i].clip = _soundDictionary[idx];
                    effSource[i].volume = volume;
                    effSource[i].Play();
                    return;
                }
            }
        }
    }

    public void OffEffSound(SOUND idx)
    {
        for (int i = 0; i < effSource.Length; ++i)
        {
            if (effSource[i].isPlaying && effSource[i].clip == _soundDictionary[idx])
            {
                effSource[i].Stop();
                return;
            }
        }
    }

    public void PlayBGMSound(SOUND idx)
    {
        bgmSource.clip = _soundDictionary[idx];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SetPlaySpeed(float speed)
    {
        bgmSource.pitch = speed;
    }
}
