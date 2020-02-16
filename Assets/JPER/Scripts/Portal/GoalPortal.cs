using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPortal : AbstPortal
{
    public override void UsePortal(PlayerMove calledPlayer)
    {
        Debug.Log("GoalPortal.UsePortal() : Goal in!");
    }
}
