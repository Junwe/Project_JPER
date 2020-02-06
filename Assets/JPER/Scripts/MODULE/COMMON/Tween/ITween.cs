using System.Collections;
using System.Collections.Generic;

public interface ITween
{
    float Time
    {
        set;
        get;
    }
    void StartTween();
    void End();

    void ReversePlay();
}
