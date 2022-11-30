using Sirenix.OdinInspector;
using UnityEngine;

public class Item
{
    public Sprite Icon;
    public int Id;
    public string Name;
    public int Price;
    public ItemType Type;
    public int Power;
    public bool Stackable;
    
    [ShowIf("Stackable")]
    public int Amount;
    
    public Item(ItemData data)
    {
        this.Id = data.Id;
        this.Name = data.Name;
        this.Price = data.Price;
        this.Type = data.Type;
        this.Power = data.Power;
        this.Stackable = data.Stackable;
        this.Icon = data.Icon;
    }

}

public enum ItemType
{
    Food,
    Furniture,
    Clothes
}