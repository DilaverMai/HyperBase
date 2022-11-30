using System;
using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Inventory inventory => playerInventory.TheInventory;
    public ItemSlot[,] ItemSlots;
    private void Awake()
    {
       var itemSlots = GetComponentsInChildren<ItemSlot>();
       
       ItemSlots = new ItemSlot[inventory.XSize, inventory.YSize];
       
       for (int i = 0; i < inventory.XSize; i++)
       {
           for (int j = 0; j < inventory.YSize; j++)
           {
               ItemSlots[i, j] = itemSlots[i + j];
               //Debug.Log( "Index i " + i + " j " + j +" = Item ındex"+ (i + j));
           }
       }
       
       
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < ItemSlots.GetLength(0); i++)
        {
            for (int j = 0; j < ItemSlots.GetLength(1); j++)
            {
                if(inventory.InventoryGrid[i, j] != null)
                    ItemSlots[i, j].UpdateItem(inventory.InventoryGrid[i, j]);
            }
        }
        
        // for (int i = 0; i < inventory.GetItemCount(); i++)
        // {
        //     
        // }
    }
}