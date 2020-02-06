using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickBtnTween : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private ITween _clickTween;
    [SerializeField]
    float _scale = 1f;
        [SerializeField]
    AnimationCurve _curve;
    // Start is called before the first frame update
    void Start()
    {
        float plus = _scale >= 0 ? 0.2f : -0.2f;
        TweenScale tween = gameObject.AddComponent(typeof(TweenScale)) as TweenScale;
        tween.SetTween(new Vector2(_scale, _scale), new Vector2(_scale + plus, _scale + plus), 0.1f, _curve);
        _clickTween = tween;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _clickTween.StartTween();
        Sound.Instance.PlayEffSound(SOUND.S_BTN);
    }
    public void OnPointerUp(PointerEventData eventDate)
    {
        _clickTween.ReversePlay();
    }
}
