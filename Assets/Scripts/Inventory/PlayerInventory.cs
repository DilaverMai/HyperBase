using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory TheInventory;
    public ItemData testitem;
    private void Awake()
    {
        var item = new Item(testitem);
        TheInventory = new Inventory(6,6);
        TheInventory.InventoryGrid[3, 4] = item;
    }
}