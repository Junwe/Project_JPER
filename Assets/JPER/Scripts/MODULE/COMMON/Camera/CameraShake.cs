using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoDestorySingleton<CameraShake>
{
    Vector3 originPos;

    void Start()
    {
        originPos = transform.localPosition;
    }

    public void Shake(float _amount, float _duration)
    {
        StartCoroutine(doShake(_amount, _duration));
    }

    private IEnumerator doShake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }
}
