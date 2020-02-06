using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// - 시작 action을 갖는 팝업이 상속받는 클래스. base에 모든 기능을 상속받고 StartAction을 가져 enable만 재정의 해서 사용.
// - 인자값이 늘어나면 대공사를 해야하지만 인자값이 늘어나면 그건 어쩔수 없는듯.. 
// - 그냥 object를 사용하니 여러개의 데이터를 사용할 경우 클래스를 새로 만들고 캐싱해서 사용하는게 더 편해보임
// - 아마도 데코레이션 패턴?
public class POPUpHasAction : POPUP_Base
{
    StartAction _action;
    
    new public void Enable(object value)
    {
        base.Enable();
        _action.StartMethod(value);
    }

    new public void Enable()
    {
        base.Enable();
        _action.StartMethod();
    }

    public void SetAction(StartAction action)
    {
        _action = action;
    }

}
