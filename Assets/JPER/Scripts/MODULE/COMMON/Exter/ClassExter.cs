using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public static class ClassExter
{
    public static void Invoke(this MonoBehaviour me, Action theDelegate, float time)
    {
        me.StartCoroutine(ExecuteAfterTime(theDelegate, time));
    }
 
    private static IEnumerator ExecuteAfterTime(Action theDelegate, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        theDelegate();
    }
    public static void Swap(this ref Color color,ref Color e)
    {
        Color temp = color;
        color = e;
        e = temp;
    }
    public static void Swap(this ref Vector3 vector3,ref Vector3 e)
    {
        Vector3 temp = vector3;
        vector3 = e;
        e = temp;
    }
    public static void Swap(this ref Vector2 vector2,ref Vector2 e)
    {
        Vector2 temp = vector2;
        vector2 = e;
        e = temp;
    }

    public static void Swap(this ref float s, ref float e)
    {
        float temp = s;
        s = e;
        e = temp;
    }

    public static Color ColorLerp(this Color colorA, Color colorB, float t)
    {
        float r = Mathf.Lerp(colorA.r,colorB.r,t);
        float g = Mathf.Lerp(colorA.g,colorB.g,t);
        float b = Mathf.Lerp(colorA.b,colorB.b,t);
        float a = Mathf.Lerp(colorA.a,colorB.a,t);

        Color newColor = new Color(r,g,b,a);

        return newColor;
    }

    public static void SetColor(this SpriteRenderer sprite, Color color)
    {
        sprite.color = color;
    }

    public static void SetAlpha(this SpriteRenderer sprite, float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static void SetAlpha(this Image sprite, float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static void SetAlpha(this Text sprite, float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static void PlusText(this Text text, int start, int end, float time, float Delay)
    {
        text.StartCoroutine(Tween.Instance.SetText(text, start, end, time, Delay));
    }
}
