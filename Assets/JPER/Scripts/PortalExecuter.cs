using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalExecuter : MonoBehaviour
{
    [SerializeField]
    private Text jumpText;

    private string prevButtonText;

    public AbstPortal CurrentPortal { get; private set; }

    public void EntryPortal(AbstPortal newPortal)
    {
        prevButtonText = jumpText.text;
        jumpText.text = "포탈 사용";
        CurrentPortal = newPortal;
    }

    public void ExitPortal()
    {
        jumpText.text = prevButtonText;
        CurrentPortal = null;
    }

    public void ExecutePortal()
    {
        if (CurrentPortal == null)
            return;

        CurrentPortal.UsePortal(this);
    }
}
