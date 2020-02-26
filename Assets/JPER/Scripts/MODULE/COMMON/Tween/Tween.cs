using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void DelayMethod();

public class Tween : MonoSingleton<Tween>
{
    public IEnumerator SetAlphaDontScale(SpriteRenderer spr, float startAlpha, float EndAlpha, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator MoveDontScale(GameObject obj, Vector3 StartPos, Vector3 EndPos, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
            yield return null;
        }
        obj.transform.localPosition = EndPos;
    }

    public IEnumerator Move(GameObject obj, Vector3 StartPos, Vector3 EndPos, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        yield return new WaitForSecondsRealtime(DelayMove);
        obj.transform.localPosition = StartPos;
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
            yield return null;
        }
        obj.transform.localPosition = EndPos;
    }

    public IEnumerator MoveLoop(GameObject obj, Vector3 StartPos, Vector3 EndPos, float time, float DelayMove, float StartDleay, AnimationCurve animationCurve)
    {
        float t = 0f;
        yield return new WaitForSecondsRealtime(StartDleay);
        obj.transform.localPosition = StartPos;
        while (true)
        {
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
                yield return null;
            }
            t = 0f;
            yield return new WaitForSecondsRealtime(DelayMove);
        }
    }

    public IEnumerator MovePingPong(GameObject obj, Vector3 StartPos, float speed, float time, float DelayMove, bool moveX, bool moveY)
    {
        float t = 0f;
        bool isdirection = true;
        obj.transform.localPosition = StartPos;
        yield return new WaitForSecondsRealtime(DelayMove);
        while (true)
        {
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);

                if (isdirection)
                    obj.transform.localPosition += new Vector3(moveX ? speed * Time.deltaTime : 0f, moveY ? speed * Time.deltaTime : 0f, 0f);
                else
                    obj.transform.localPosition -= new Vector3(moveX ? speed * Time.deltaTime : 0f, moveY ? speed * Time.deltaTime : 0f, 0f);

                yield return null;
            }
            t = 0f;
            isdirection = !isdirection;
        }
    }

    public IEnumerator Move(RectTransform obj, Vector3 StartPos, Vector3 EndPos, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        obj.transform.localPosition = StartPos;
        yield return new WaitForSecondsRealtime(DelayMove);

        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
            yield return null;
        }
        obj.transform.localPosition = EndPos;
    }

    public IEnumerator MoveLoop(RectTransform obj, Vector3 StartPos, Vector3 EndPos, float time, float DelayMove, float startDelay, AnimationCurve animationCurve)
    {
        float t = 0f;
        obj.transform.localPosition = StartPos;
        yield return new WaitForSecondsRealtime(startDelay);
        while (true)
        {
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
                yield return null;
            }
            t = 0f;
            yield return new WaitForSecondsRealtime(DelayMove);
        }
    }

    public IEnumerator SetAlpha(SpriteRenderer spr, float startAlpha, float EndAlpha, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.SetAlpha(startAlpha);
        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator SetAlphaLoop(SpriteRenderer spr, float startAlpha, float EndAlpha, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.SetAlpha(startAlpha);
        while (true)
        {
            yield return new WaitForSecondsRealtime(DelayMove);
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
                yield return null;
            }
            t = 0f;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator SetColor(SpriteRenderer spr, Color StartColor, Color EndColor, float time, float DelayMove)
    {
        float t = 0f;
        AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        spr.SetColor(StartColor);
        yield return new WaitForSecondsRealtime(DelayMove);

        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            float r = Mathf.Lerp(StartColor.r, EndColor.r, curve.Evaluate(t));
            float g = Mathf.Lerp(StartColor.g, EndColor.g, curve.Evaluate(t));
            float b = Mathf.Lerp(StartColor.b, EndColor.b, curve.Evaluate(t));
            float a = Mathf.Lerp(StartColor.a, EndColor.a, curve.Evaluate(t));

            spr.SetColor(new Color(r, g, b, a));
            yield return null;
        }
    }

    public IEnumerator SetColorPingPong(SpriteRenderer spr, Color StartColor, Color EndColor, float time, int count)
    {
        float t = 0f;
        AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        spr.SetColor(StartColor);
        Color oneColor = StartColor;
        for (int i = 0; i < count * 2; ++i)
        {
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                float r = Mathf.Lerp(StartColor.r, EndColor.r, curve.Evaluate(t));
                float g = Mathf.Lerp(StartColor.g, EndColor.g, curve.Evaluate(t));
                float b = Mathf.Lerp(StartColor.b, EndColor.b, curve.Evaluate(t));
                float a = Mathf.Lerp(StartColor.a, EndColor.a, curve.Evaluate(t));

                spr.SetColor(new Color(r, g, b, a));
                yield return null;
            }
            t = 0f;
            Color temp = StartColor;
            StartColor = EndColor;
            EndColor = temp;
        }

        spr.SetColor(oneColor);
    }


    public IEnumerator SetAlpha(Image spr, float startAlpha, float EndAlpha, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.SetAlpha(startAlpha);
        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
    }

    public IEnumerator SetAlphaLoop(Image spr, float startAlpha, float EndAlpha, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.SetAlpha(startAlpha);
        while (true)
        {
            yield return new WaitForSecondsRealtime(DelayMove);
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
                yield return null;
            }
            t = 0f;
        }
    }

    public IEnumerator SetAlpha(Text spr, float startAlpha, float EndAlpha, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.SetAlpha(startAlpha);
        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator SetAlphaLoop(Text spr, float startAlpha, float EndAlpha, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;

        spr.SetAlpha(startAlpha);
        while (true)
        {
            yield return new WaitForSecondsRealtime(DelayMove);
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
                yield return null;
            }
            t = 0f;
        }
    }

    public IEnumerator SetScale(RectTransform spr, Vector2 StartScale, Vector2 EndScale, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.sizeDelta = StartScale;
        yield return new WaitForSecondsRealtime(DelayMove);

        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            spr.sizeDelta = StartScale + (EndScale - StartScale) * animationCurve.Evaluate(t);
            yield return null;
        }
        spr.sizeDelta = EndScale;
    }

    public IEnumerator SetScaleLoop(RectTransform spr, Vector2 StartScale, Vector2 EndScale, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.sizeDelta = StartScale;
        while (true)
        {
            yield return new WaitForSecondsRealtime(DelayMove);
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                spr.sizeDelta = StartScale + (EndScale - StartScale) * animationCurve.Evaluate(t);
                yield return null;
            }
            t = 0f;
        }
    }

    public IEnumerator SetLineRenderWith(LineRenderer line, float StartScale, float EndScale, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        line.startWidth = StartScale;
        line.endWidth = StartScale;

        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            line.startWidth = StartScale + (EndScale - StartScale) * animationCurve.Evaluate(t);
            line.endWidth = StartScale + (EndScale - StartScale) * animationCurve.Evaluate(t);
            yield return null;
        }
        line.startWidth = EndScale;
        line.endWidth = EndScale;
        //line.gameObject.SetActive(false);
    }

    public IEnumerator SetScale(Transform spr, Vector2 StartScale, Vector2 EndScale, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.localScale = StartScale;
        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            spr.localScale = StartScale + (EndScale - StartScale) * animationCurve.Evaluate(t);// Vector2.Lerp(StartScale, EndScale, animationCurve.Evaluate(t));
            yield return null;
        }
        spr.localScale = EndScale;
    }

    public IEnumerator SetScaleLoop(Transform spr, Vector2 StartScale, Vector2 EndScale, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        spr.localScale = StartScale;
        while (true)
        {
            yield return new WaitForSecondsRealtime(DelayMove);
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                spr.localScale = StartScale + (EndScale - StartScale) * animationCurve.Evaluate(t);
                yield return null;
            }
            t = 0f;
        }
    }

    public IEnumerator SetText(Text txt, int start, int end, float time, float Delay)
    {
        float t = 0f;
        txt.text = start.ToString();
        yield return new WaitForSecondsRealtime(Delay);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            txt.text = ((int)(start + (end - start) * t)).ToString();

            yield return null;
        }
        t = 0f;
    }

    public IEnumerator DelayMethod(float time, DelayMethod delayMethod)
    {
        yield return new WaitForSecondsRealtime(time);

        delayMethod();
    }

    public IEnumerator CameraSize(Camera camera, float Startsize, float Endsize, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        camera.orthographicSize = Startsize;

        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            camera.orthographicSize = Mathf.Lerp(Startsize, Endsize, animationCurve.Evaluate(t));
            yield return null;
        }
        t = 0f;

    }

    public IEnumerator SprtieAnimation(SpriteRenderer spriterenderer, Sprite[] sprites, float time, float DelayMove)
    {
        float t = 0f;
        int count = 0;
        yield return new WaitForSecondsRealtime(DelayMove);
        while (count < sprites.Length)
        {
            while (t < 1.0f)
            {
                t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                spriterenderer.sprite = sprites[count];
                yield return null;
            }
            count++;
            t = 0f;
        }
        spriterenderer.gameObject.SetActive(false);
    }

    public IEnumerator SprtieAnimationLoop(SpriteRenderer spriterenderer, Sprite[] sprites, float time, float DelayMove)
    {
        float t = 0f;
        int count = 0;
        yield return new WaitForSecondsRealtime(DelayMove);

        while (true)
        {
            while (count < sprites.Length)
            {
                while (t < 1.0f)
                {
                    t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
                    spriterenderer.sprite = sprites[count];
                    yield return null;
                }
                count++;
                t = 0f;
            }
            count = 0;
        }
    }

    public IEnumerator SetAmount(Image image, float StartScale, float EndScale, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        image.fillAmount = StartScale;
        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            image.fillAmount = Mathf.Lerp(StartScale, EndScale, animationCurve.Evaluate(t));
            yield return null;
        }
        image.fillAmount = EndScale;
    }

    public IEnumerator SetRoation(GameObject obj, float startRoation, float EndRoation, float time, float DelayMove, AnimationCurve animationCurve)
    {
        float t = 0f;
        yield return new WaitForSecondsRealtime(DelayMove);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            obj.transform.localEulerAngles = new Vector3(obj.transform.localEulerAngles.x, obj.transform.localEulerAngles.y,
            Mathf.Lerp(startRoation, EndRoation, animationCurve.Evaluate(t)));
            yield return null;
        }
        obj.transform.localEulerAngles = new Vector3(obj.transform.localEulerAngles.x, obj.transform.localEulerAngles.y, EndRoation);
    }

    public IEnumerator SetRoationLoop(GameObject obj, float startRoation, float roationSpeed, float DelayMove)
    {
        obj.transform.localEulerAngles = new Vector3(obj.transform.localEulerAngles.x, obj.transform.localEulerAngles.y, startRoation);
        yield return new WaitForSecondsRealtime(DelayMove);
        while (true)
        {
            obj.transform.localEulerAngles += new Vector3(0f,0f,roationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}