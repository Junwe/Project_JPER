using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenAlpha : MonoBehaviour, ITween
{
    enum TWEENTYPE
    {
        SPRITE,
        TXT,
        IMG,
    }

    [SerializeField]
    float _start;
    [SerializeField]
    float _end;
    [SerializeField]
    float _time;
    [SerializeField]
    float _delay;
    [SerializeField]
    AnimationCurve _curve;
    [SerializeField]
    bool _isAwake;
    [SerializeField]
    bool _isLoop;
    [SerializeField]
    TWEENTYPE _type;

    Coroutine _crt;

    public float Time
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
        }
    }

    public void End()
    {
        if (_crt != null)
        {
            StopCoroutine(_crt);
            GetComponent<SpriteRenderer>().SetAlpha(_end);
        }
    }

    public void StartTween()
    {
        if (_isLoop)
        {
            if (_type == TWEENTYPE.IMG)
                _crt = StartCoroutine(Tween.Instance.SetAlphaLoop(GetComponent<Image>(), _start, _end, _time, _delay, _curve));
            if (_type == TWEENTYPE.SPRITE)
                _crt = StartCoroutine(Tween.Instance.SetAlphaLoop(GetComponent<SpriteRenderer>(), _start, _end, _time, _delay, _curve));
            if (_type == TWEENTYPE.TXT)
                _crt = StartCoroutine(Tween.Instance.SetAlphaLoop(GetComponent<Text>(), _start, _end, _time, _delay, _curve));
        }
        else
        {
            if (_type == TWEENTYPE.IMG)
                _crt = StartCoroutine(Tween.Instance.SetAlpha(GetComponent<Image>(), _start, _end, _time, _delay, _curve));
            if (_type == TWEENTYPE.SPRITE)
                _crt = StartCoroutine(Tween.Instance.SetAlpha(GetComponent<SpriteRenderer>(), _start, _end, _time, _delay, _curve));
            if (_type == TWEENTYPE.TXT)
                _crt = StartCoroutine(Tween.Instance.SetAlpha(GetComponent<Text>(), _start, _end, _time, _delay, _curve));
        }
    }

    public void ReversePlay()
    {
        _start.Swap(ref _end);
        StartTween();
        _start.Swap(ref _end);
    }

    void Start()
    {
        if (_isAwake)
            StartTween();
    }

}
