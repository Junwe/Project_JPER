using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstPortal : MonoBehaviour
{
    public abstract void UsePortal(PortalExecuter calledExecuter);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var portalExecuter = collision.GetComponent<PortalExecuter>();
        if (portalExecuter != null)
            portalExecuter.EntryPortal(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var portalExecuter = collision.GetComponent<PortalExecuter>();
        if (portalExecuter != null)
            portalExecuter.ExitPortal();
    }
}
