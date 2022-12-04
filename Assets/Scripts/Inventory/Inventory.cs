using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public List<Item> InventoryGrid;
    public int Gold;
    public int InventorySize;
    public void CreateInventory(int size)
    {
        InventoryGrid = new List<Item>(InventorySize);
        
        for (var x = 0; x < InventorySize; x++)
        {
            InventoryGrid.Add(null);
        }
    }
}
