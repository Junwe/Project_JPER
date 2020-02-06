using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


public class StartAction
{
    UnityAction<object> _startActionObject;
    UnityAction _startAction;
    public void StartMethod()
    {
        _startAction();
    }
    public void StartMethod(object value)
    {
        _startActionObject(value);
    }

    public StartAction(UnityAction<object> action)
    {
        _startActionObject = action;
    }
    public StartAction(UnityAction action)
    {
        _startAction = action;
    }
}
