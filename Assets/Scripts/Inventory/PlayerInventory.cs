using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory TheInventory;
    public ItemData testitem;
    private void Awake()
    {
        TheInventory.CreateInventory(25);
        TheInventory.InventoryGrid[0] = new Item(testitem);
    }
}