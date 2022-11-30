using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData: ScriptableObject
{
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite Icon;
    [VerticalGroup("Split/Properties")]
    public int Id;
    [VerticalGroup("Split/Properties")]
    public string Name;
    [VerticalGroup("Split/Properties")]
    public int Price;
    
    public ItemType Type;
    public int Power;
    
    public bool Stackable;
    [ShowIf("Stackable")]
    public int Amount;
}