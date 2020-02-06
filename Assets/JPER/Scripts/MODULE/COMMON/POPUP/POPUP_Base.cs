using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// 모든 팝업에 기본이 되는 베이스.
// enable, disable을 기본으로 가짐
// 재정의가 필요한 경우 base를 상속받고 enable, disable을 재정의.
public class POPUP_Base : MonoBehaviour, IPopUp
{
    public TweenScale _openTween;
    public GameObject obj
    {
        get { return gameObject;}
    }

    string _disableEvent;

    public string DisableEvent
    {
        get { return _disableEvent; }
        set { _disableEvent = value; }
    }

    public void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);
        gameObject.transform.localScale = Vector3.zero;
    }

    public virtual void Enable()
    {
        //_openTween.Time = 0.25f;
        _openTween.StartTween();
    }

    public virtual void Enable(object value)
    {
        Enable();
    }

    public virtual void Disable()
    {
        //_openTween.Time = 0.25f;
        _openTween.ReversePlay();
    }
}
