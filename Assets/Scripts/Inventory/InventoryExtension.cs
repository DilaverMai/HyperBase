using System.Linq;
using UnityEngine;

public static class InventoryExtension
{
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
    
    public static void ClearGold(this Inventory inventory)
    {
        inventory.Gold = 0;
    }
    
    public static int GetItemCount(this Inventory inventory)
    {
        return inventory.InventoryGrid.Count(item => item != null);;
    }
    
    public static void MoveItem(this Inventory inventory,Item item,int newIndex)
    {
        inventory.InventoryGrid.Remove(item);
        inventory.InventoryGrid.Insert(newIndex,item);
    }
    
}