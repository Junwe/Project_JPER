using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SystemMsg : MonoBehaviour
{
    public TweenAlpha alphaTween;
    public TweenMove moveTween;
    public Text txtMsg;

    public void OnMsg(string msg)
    {
        moveTween.StopAllCoroutines();
        alphaTween.StopAllCoroutines();
        txtMsg.text = msg;
        alphaTween.StartTween();
        moveTween.StartTween();
    }
}
