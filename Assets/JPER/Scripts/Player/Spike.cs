using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spike : MonoBehaviour, ITrapExecute
{
    public void Execute(PlayerMove targetPlayer)
    {
        int direction = transform.position.x - targetPlayer.transform.position.x > 0 ? -1 : 1;
        targetPlayer.OnKnockback(direction, verticalAcceleration: .4f, horizontalAcceleration: .4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null)
            Execute(player);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null && player.KnuckbackFlag == false)
            Execute(player);
    }
}
