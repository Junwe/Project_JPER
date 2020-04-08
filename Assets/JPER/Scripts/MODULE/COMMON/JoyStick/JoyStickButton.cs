using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JoyStickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    bool _isActiveAlpha = false;
    private bool _isDraging = false;
    private List<UnityAction> _moveEventAction = new List<UnityAction>();
    private List<UnityAction> _UpEventAction = new List<UnityAction>();

    private Button _myButton;
    private Image _myImage;

    void Awake()
    {
        _myButton = GetComponent<Button>();
        _myImage = GetComponent<Image>();
    }
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
        if (_isActiveAlpha)
            _myImage.SetAlpha(1f);
    }

    public void OnUp()
    {
        _isDraging = false;
        foreach (var action in _UpEventAction)
        {
            action();
        }
        if (_isActiveAlpha)
            _myImage.SetAlpha(0.5f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDraging = true;
        if (_isActiveAlpha)
            _myImage.SetAlpha(1f);
    }
    public void OnPointerUp(PointerEventData eventDate)
    {
        _isDraging = false;
        foreach (var action in _UpEventAction)
        {
            action();
        }
        if (_isActiveAlpha)
            _myImage.SetAlpha(0.5f);
    }
}
