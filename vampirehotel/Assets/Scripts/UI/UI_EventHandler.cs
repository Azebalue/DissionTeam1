using System;
using UnityEngine;
using UnityEngine.EventSystems;

//UI에서 버튼 기능 컴포넌트 대체하는 클래스
public class UI_EventHandler : MonoBehaviour ,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //상호작용 함수Handler
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnEnterHandler = null;
    public Action<PointerEventData> OnExitHandler = null;

    //상호작용 발생 메서드들
    public void OnPointerClick(PointerEventData eventData)
    {

        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnEnterHandler != null)
            OnEnterHandler.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnExitHandler != null)
            OnExitHandler.Invoke(eventData);
    }
}