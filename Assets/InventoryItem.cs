using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,IPointerDownHandler,IPointerMoveHandler,IPointerUpHandler
{
    public Image icon;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI nameText;

    public int SlotIndex;
    private bool selected;
    public Item item;

    private Vector2 startPos;
    public void SetItem(Item newItem,int slotIndex)
    {
        SlotIndex = slotIndex;
        
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        UpdateItem(ref newItem);
    }

    public void UpdateItem(ref Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.Icon;
        icon.enabled = true;
        priceText.text = newItem.Price.ToString();
        amountText.text = newItem.Amount.ToString();
        nameText.text = newItem.Name;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        priceText.text = "";
        amountText.text = "";
        nameText.text = "";
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = transform.position;
        
        if(item == null)
            return;
        selected = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(!selected) 
            return;
        transform.parent.SetSiblingIndex(transform.parent.parent.childCount - 1);
        transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!selected)
            return;

        if(Vector3.Distance(transform.position,startPos) < 100f)
        {
            transform.DOLocalMove(Vector3.zero, 0.2f);
            selected = false;
            return;
        }
        
        if (!InventoryUI.Instance.PutItemInInventory(transform.position,this))
        {
            transform.DOLocalMove(Vector3.zero, 0.2f);
        }
        
        transform.localPosition = Vector3.zero;
        selected = false;
    }
}