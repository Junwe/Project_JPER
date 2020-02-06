using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeScaleMode{
    SCALE = 0,
    UNSCALE = 1
}
public class TimeCounter
{
    public float counter;
    public float Timer;

    private TimeScaleMode _scaleMode;

    public TimeCounter()
    {
        
    }
    public TimeCounter(float counter, float timer, TimeScaleMode mode = TimeScaleMode.SCALE)
    {
        this.counter = counter;
        this.Timer = timer;

        _scaleMode = mode;
    }

    public void SetTimeCounter(float counter, float timer)
    {
        this.counter = counter;
        this.Timer = timer;
    }

    public float GetRegularTime()
    {
        return Mathf.Clamp01(counter / Timer);
    }

    public bool CheckOverTime(float deltaTime)
    {
        counter += deltaTime;
        if(counter >= Timer)
        {
            counter = 0f;
            return true;
        }
        else
            return false;
    }
}
