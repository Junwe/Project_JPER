using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPortal : AbstPortal
{
    public override void UsePortal(PortalExecuter calledExecuter)
    {
        Debug.Log("GoalPortal.UsePortal() : Goal in!");
    }
}
