using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstPortal : MonoBehaviour
{
    public abstract void UsePortal(PlayerMove calledPlayer);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null)
            player.EntryPortal(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null)
            player.ExitPortal();
    }
}
