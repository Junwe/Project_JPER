using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalExecuter : MonoBehaviour
{
    public AbstPortal CurrentPortal { get; private set; }

    public void EntryPortal(AbstPortal newPortal)
    {
        CurrentPortal = newPortal;
    }

    public void ExitPortal()
    {
        CurrentPortal = null;
    }

    public void ExecutePortal()
    {
        if (CurrentPortal == null)
            return;

        CurrentPortal.UsePortal(this);
    }
}
