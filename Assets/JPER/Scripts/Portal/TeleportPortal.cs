using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : AbstPortal
{
    public Transform destination;

    public override void UsePortal(PlayerMove calledPlayer)
    {
        calledPlayer.transform.position = destination.position;
    }
}
