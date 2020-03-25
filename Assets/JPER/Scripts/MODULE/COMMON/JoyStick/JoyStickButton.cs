using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JoyStickButton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
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

    public void OnDrag(PointerEventData eventData)
    {
        if (_moveEventAction == null)
        {
            Debug.LogError(gameObject.name + " : _moveEventAction is null");
            return;
        }
        _isDraging = true;
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

    public void OnPointerEnter(PointerEventData data)
    {
#if !UNITY_EDITOR
        _isDraging = true;
#endif
    }
    public void OnPointerExit(PointerEventData eventData)
    {
#if !UNITY_EDITOR
        _isDraging = false;
        foreach (var action in _UpEventAction)
        {
            action();
        }
#endif
    }

}
