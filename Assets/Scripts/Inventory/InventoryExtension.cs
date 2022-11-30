using System.Linq;
using UnityEngine;

public static class InventoryExtension
{
    // public static void AddItem(this Inventory inventory, Item item)
    // {
    //     inventory.Items.Add(item);
    // }
    //
    // public static void RemoveItem(this Inventory inventory, Item item)
    // {
    //     inventory.Items.Remove(item);
    // }
    
    // public static bool HasItem(this Inventory inventory, Item item)
    // {
    //     return inventory.Items.Contains(item);
    // }
    //
    // public static bool InventoryFull(this Inventory inventory,int maxSize)
    // {
    //     return inventory.Items.Count == maxSize;
    // }
    
    public static void AddGold(this Inventory inventory, int gold)
    {
        inventory.Gold += gold;
    }
    
    public static void RemoveGold(this Inventory inventory, int gold)
    {
        inventory.Gold -= gold;
    }
    
    public static bool HasGold(this Inventory inventory, int gold)
    {
        return inventory.Gold >= gold;
    }
    
    // public static void ClearInventory(this Inventory inventory)
    // {
    //     inventory.Items.Clear();
    // }
    //
    public static void ClearGold(this Inventory inventory)
    {
        inventory.Gold = 0;
    }
    
    public static int GetItemCount(this Inventory inventory)
    {
        return inventory.InventoryGrid.Cast<Item>().Count(i => i != null);
    }
    
    // public static void ClearAll(this Inventory inventory)
    // {
    //     inventory.ClearInventory();
    //     inventory.ClearGold();
    // }
    //
    // public static void IncreaseMaxSize(this Inventory inventory, int amount)
    // {
    //     inventory.MaxSize += amount;
    // }
    //
    // public static void DecreaseMaxSize(this Inventory inventory, int amount)
    // {
    //     inventory.MaxSize -= amount;
    // }
    
    // public static void PrintInventory(this Inventory inventory)
    // {
    //     Debug.Log("Inventory:");
    //     foreach (var item in inventory.Items)
    //     {
    //         Debug.Log(item.Name);
    //     }
    //     Debug.Log("Gold: " + inventory.Gold);
    // }
}