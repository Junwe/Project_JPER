using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalExecuter : MonoBehaviour
{
    private AbstPortal _currentPortal = null;

    public void EntryPortal(AbstPortal newPortal)
    {
        _currentPortal = newPortal;
    }

    public void ExitPortal()
    {
        _currentPortal = null;
    }

    public void ExecutePortal()
    {
        if (_currentPortal == null)
            return;

        _currentPortal.UsePortal(this);
    }
}
