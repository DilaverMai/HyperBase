using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

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
}

[System.Serializable]
public class DropItem
{
    public Item Item;
    [Range(0, 100)]
    public int Amount;
    public bool IsRandom;
    
    public void DropTheItem([Optional]Vector3 pos)
    {
        var amount = Amount;
        if(IsRandom) amount = Random.Range(0, Amount);
        
        if (Item.ItemType == ItemType.Coin)
        {
            Debug.Log("Gold Coming");
            ParticleExtension.PlayCoinEffect(pos,amount);
            Datas.Coin.CoinAdd(amount);
            return;
        }
        
        var spawn = GameObject.Instantiate(Item.Prefab, pos, Quaternion.identity);
    }
}