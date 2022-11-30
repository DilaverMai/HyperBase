using Base;
using DBase;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickArea : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.FirstTouch.Invoke();
    }
}
