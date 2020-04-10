using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionCounter
{
    public enum RecordDataType
    {
        None = -1,
        PlayTime,
        FallCount,
        JumpCount,
        MovedDistance,
        HitCount
    }

    public float PlayTime { get; private set; } = 0;
    public int FallCount { get; private set; } = 0;
    public int JumpCount { get; private set; } = 0;
    public float MovedDistance { get; private set; } = 0;

    public int HitCount { get; private set; } = 0;

    public void AddFallCount()
    {
        ++FallCount;
    }

    public void AddJumpCount()
    {
        ++JumpCount;
    }

    public void AddPlayTime(float time)
    {
        PlayTime += time;
    }

    public void AddMoveDisctcne(float distance)
    {
        MovedDistance += distance;
    }

    public void IncreasMovedDistance(float distancePerFrame)
    {
        MovedDistance += distancePerFrame;
    }

    public void AddHitCount()
    {
        HitCount++;
    }

    public string GetValueString(RecordDataType dataType)
    {
        switch (dataType)
        {
            case RecordDataType.PlayTime:
                return (Mathf.Floor(PlayTime * 100f) / 100f).ToString() + "s";
            case RecordDataType.FallCount:
                return FallCount.ToString();
            case RecordDataType.JumpCount:
                return JumpCount.ToString();
            case RecordDataType.MovedDistance:
                return MovedDistance.ToString() + "M";
        }

        return null;
    }
}
