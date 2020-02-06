using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSprtie : MonoBehaviour, ITween
{
    [SerializeField]
    Sprite[] _sprites;
    [SerializeField]
    float _time;
    [SerializeField]
    float _delay;
    [SerializeField]
    float _startDleay;
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
            GetComponent<SpriteRenderer>().sprite = _sprites[_sprites.Length - 1];
        }
    }

    public void StartTween()
    {
        End();
        gameObject.SetActive(true);
        if (_isLoop)
        {
            _crt = StartCoroutine(Tween.Instance.SprtieAnimationLoop(GetComponent<SpriteRenderer>(), _sprites, _time, _delay));
        }
        else
        {
            _crt = StartCoroutine(Tween.Instance.SprtieAnimation(GetComponent<SpriteRenderer>(), _sprites, _time, _delay));
        }
    }

    public void ReversePlay()
    {
        End();
        StartTween();
    }

    void Start()
    {
        if (_isAwake)
            StartTween();
    }
}
