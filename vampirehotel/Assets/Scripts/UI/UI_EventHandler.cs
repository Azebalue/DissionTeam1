using System;
using UnityEngine;
using UnityEngine.EventSystems;

//UI���� ��ư ��� ������Ʈ ��ü�ϴ� Ŭ����
public class UI_EventHandler : MonoBehaviour ,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //��ȣ�ۿ� �Լ�Handler
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnEnterHandler = null;
    public Action<PointerEventData> OnExitHandler = null;

    //��ȣ�ۿ� �߻� �޼����
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