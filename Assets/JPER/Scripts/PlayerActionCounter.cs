using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionCounter
{
    public float PlayTime { get; private set; } = 0;
    public int FallCount { get; private set; } = 0;
    public int JumpCount { get; private set; } = 0;
    public float MovedDistance { get; private set; } = 0;

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

    public void IncreasMovedDistance(float distancePerFrame)
    {
        MovedDistance += distancePerFrame;
    }
}
