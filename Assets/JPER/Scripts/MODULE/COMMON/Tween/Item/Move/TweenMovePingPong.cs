using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMovePingPong : MonoBehaviour, ITween
{
    [SerializeField]
    Vector3 _start;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _time;
    [SerializeField]
    float _delay;
    [SerializeField]
    bool _isAwake;
    [SerializeField]
    bool _moveX;
    [SerializeField]
    bool _moveY;
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
        }
    }

    public void StartTween()
    {
        StartCoroutine(Tween.Instance.MovePingPong(gameObject, _start, _speed, _time, _delay, _moveX, _moveY));
    }

    public void ReversePlay()
    {
        
    }

    void Start()
    {
        if (_isAwake)
            StartTween();
    }
}
