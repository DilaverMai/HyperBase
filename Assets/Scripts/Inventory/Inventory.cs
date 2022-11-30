using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public int XSize, YSize;
    public Item[,] InventoryGrid;
    public int Gold;
    
    public Inventory(int xSize, int ySize)
    {
        XSize = xSize;
        YSize = ySize;
        InventoryGrid = new Item[XSize, YSize];
    }
}