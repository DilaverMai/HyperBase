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
    
    public int Amount;
    
    public Item(ItemData data)
    {
        if(!data) return;
        this.Id = data.Id;
        this.Name = data.Name;
        this.Price = data.Price;
        this.Type = data.Type;
        this.Power = data.Power;
        this.Stackable = data.Stackable;
        this.Icon = data.Icon;
        this.Amount = data.Amount;
    }
    
    public void LoadItemData(ItemData data)
    {
        this.Id = data.Id;
        this.Name = data.Name;
        this.Price = data.Price;
        this.Type = data.Type;
        this.Power = data.Power;
        this.Stackable = data.Stackable;
        this.Icon = data.Icon;
        this.Amount = data.Amount;
    }
    
}

public enum ItemType
{
    Food,
    Furniture,
    Clothes
}