using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension 
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    { 
        UI_Base.AddUIEvent(go, action, type); //사실 나는 얘였어..?
    }

}
