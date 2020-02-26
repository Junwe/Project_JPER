using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : MonoBehaviour, ITween
{
    [SerializeField]
    float _start;
    [SerializeField]
    float _end;
    [SerializeField]
    float _time;
    [SerializeField]
    float _delay;
    [SerializeField]
    float _startDleay;
    [SerializeField]
    AnimationCurve _curve;
    [SerializeField]
    bool _isAwake;
    [SerializeField]
    bool _isLoop;

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
            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, _end);
        }
    }

    public void StartTween()
    {
        End();
        if (_isLoop)
        {
            _crt = StartCoroutine(Tween.Instance.SetRoationLoop(gameObject, _start, _end, _delay));
        }
        else
        {
            _crt = StartCoroutine(Tween.Instance.SetRoation(gameObject, _start, _end, _time, _delay, _curve));
        }
    }

    public void ReversePlay()
    {
        End();
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
