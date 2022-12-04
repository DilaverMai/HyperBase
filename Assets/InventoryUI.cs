using System;
using System.Linq;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    public PlayerInventory playerInventory;
    private Inventory inventory => playerInventory.TheInventory;
    private InventoryItem[] inventoryItems;
    private void Awake()
    {
        inventoryItems = GetComponentsInChildren<InventoryItem>();
    }
    
    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (var i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].SetItem(inventory.InventoryGrid[i],i);
        }
    }
    
    public bool PutItemInInventory(Vector2 screenPos,InventoryItem index)
    {
        var list = inventoryItems.Where(x => x != index).ToList();
        
        var select = list.OrderBy(i => 
            Vector2.Distance(i.transform.position,screenPos)).First();

        if (select == index)
        {
            Debug.Log("same");
            return false;
        }
        
        if (select.item == null)
        {
            select.UpdateItem(ref index.item);
            index.ClearSlot();
            inventory.MoveItem(select.item,select.SlotIndex);
            return true;
        }
        
        return false;
    }
    
}
