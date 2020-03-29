using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JoyStickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isDraging = false;
    private List<UnityAction> _moveEventAction = new List<UnityAction>();
    private List<UnityAction> _UpEventAction = new List<UnityAction>();
    public void SetMoveEvent(UnityAction action)
    {
        _moveEventAction.Add(action);
    }

    public void SetUpEvent(UnityAction action)
    {
        _UpEventAction.Add(action);
    }

    void FixedUpdate()
    {
        if (_isDraging)
        {
            Draging();
        }
    }

    private void Draging()
    {
        foreach (var action in _moveEventAction)
        {
            action();
        }
    }

    public void ClearAction()
    {
        _moveEventAction.Clear();
        _UpEventAction.Clear();
    }

    public void OnDown()
    {
        _isDraging = true;
    }

    public void OnUp()
    {
        _isDraging = false;
        foreach (var action in _UpEventAction)
        {
            action();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDraging = true;
    }
    public void OnPointerUp(PointerEventData eventDate)
    {
        _isDraging = false;
        foreach (var action in _UpEventAction)
        {
            action();
        }
    }
}
