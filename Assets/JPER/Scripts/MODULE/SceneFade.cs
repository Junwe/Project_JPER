using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneFade : MonoSingleton<SceneFade>
{
    public Image imgFade;
    void Start()
    {
        SceneManager.sceneLoaded += FadeOut;
    }

    private void CreateFade()
    {
        GameObject canvas = (GameObject)Resources.Load("Canvas_Fade");
        GameObject temp = Instantiate(canvas);
        temp.transform.parent = transform;
        temp.transform.position = Vector3.zero;
        imgFade = temp.GetComponentInChildren<Image>();
    }

    public void SceneLoad(object scene)
    {
        Debug.Log("SceneLoad");
        if (imgFade == null)
        {
            CreateFade();
        }
        StartCoroutine(FadeIn(scene, 0.5f));
    }

    private void FadeOut(Scene scene, LoadSceneMode mode)
    {
        if (imgFade == null)
        {
            CreateFade();
        }
        StartCoroutine(FadeOut(1f));
    }

    IEnumerator FadeIn(object scene, float time)
    {
        float t = 0f;
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            imgFade.SetAlpha(t);
            yield return new WaitForEndOfFrame();
        }
        imgFade.SetAlpha(0f);
        if (scene is int)
            SceneManager.LoadScene((int)scene);
        if (scene is string)
            SceneManager.LoadScene((string)scene);
    }

    IEnumerator FadeOut(float time)
    {
        float t = 0f;
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            imgFade.SetAlpha(1f - t);
            yield return new WaitForEndOfFrame();
        }
        imgFade.SetAlpha(0f);
    }

}
