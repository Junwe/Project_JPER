using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : AbstPortal
{
    public Transform destination;

    public override void UsePortal(PortalExecuter calledExecuter)
    {
        calledExecuter.transform.position = destination.position;
    }
}
