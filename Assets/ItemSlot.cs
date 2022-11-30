using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour,IPointerDownHandler,IPointerMoveHandler,IPointerUpHandler
{
    public Item item;
    public Image icon;
    public Button button;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI nameText;
    private Vector3 startPos;  
    private bool selected;
    
    private void Awake()
    {
        startPos = transform.position;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void UpdateItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        //button.interactable = true;
        priceText.text = item.Price.ToString();
        amountText.text = item.Amount.ToString();
        nameText.text = item.Name;
    }
    
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        //button.interactable = false;
        priceText.text = "";
        amountText.text = "";
        nameText.text = "";
    }
    
    public void OnClick()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selected = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(!selected | item == null) return;
        transform.SetSiblingIndex(transform.parent.childCount - 1);
        transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selected = false;
        transform.DOMove(startPos, 0.2f);
    }
}