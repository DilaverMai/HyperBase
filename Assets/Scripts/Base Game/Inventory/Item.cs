using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    [PreviewField(50, ObjectFieldAlignment.Right)]
    public Sprite ItemIcon;
    public GameObject Prefab;
    public ItemType ItemType;
    [HideIf("ItemType", ItemType.Coin)]
    public int ItemPower;
    [HideIf("ItemType", ItemType.Coin)]
    public int ItemLevel;
    public bool IsStackable;
    [ShowIf("IsStackable")]
    public int MaxStack;
    [ShowIf("IsStackable")]
    public int CurrentStack;
    public int SellPrice;
    public int BuyPrice;
    public bool IsEquipable;
}   


