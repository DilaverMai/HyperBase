using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using DG.Tweening;

public static class InventoryExtension
{
    public static bool IsFull(this CharacterInventory characterInventory)
    {
        return characterInventory.items.Count >= characterInventory.MaxSize;
    }

    public static Item CheckBodyItems(this ItemType itemType, CharacterInventory characterInventory, int Level)
    {
        foreach (var item in characterInventory.items)
        {
            if (item.ItemType == itemType)
            {
                if (characterInventory.Armor == null)
                    characterInventory.Armor = item;
                else if (characterInventory.Armor.ItemLevel < item.ItemLevel & characterInventory.Armor.ItemLevel < Level)
                    characterInventory.Armor = item;
            }
        }

        return null;
    }

    public static void DropRandomItem(this List<DropItem> list, [Optional] Vector3 pos)
    {
        list[Random.Range(0, list.Count)].DropTheItem(pos);
    }

    public static void AddItem(this Item item, CharacterInventory characterInventory)
    {
        if (item.IsStackable)
        {
            foreach (var SearchingItem in characterInventory.items)
            {
                if (SearchingItem == item)
                {
                    if (SearchingItem.CurrentStack < SearchingItem.MaxStack)
                        SearchingItem.CurrentStack++;
                }
            }
        }

        if (characterInventory.IsFull())
            return;

        characterInventory.items.Add(item);
    }

    public static void AddItem(this CharacterInventory characterInventory, Item item)
    {
        if (item.IsStackable)
        {
            foreach (var SearchingItem in characterInventory.items)
            {
                if (SearchingItem == item)
                {
                    if (SearchingItem.CurrentStack < SearchingItem.MaxStack)
                        SearchingItem.CurrentStack++;
                }
            }
        }

        if (characterInventory.IsFull())
            return;

        characterInventory.items.Add(item);
    }

    public static void RemoveItem(this Item item, CharacterInventory characterInventory)
    {
        characterInventory.items.Remove(item);
    }
}

[System.Serializable]
public class DropItem
{
    public Item Item;
    [Range(1, 100)] public int Amount;
    public bool IsRandom;
    [Range(1, 10)] public int Lucky;

    public void DropTheItem(Vector3 pos, bool CoinSpawn = false)
    {
        var amount = Amount;
        if (IsRandom) amount = Random.Range(0, Amount);

        if (Item.ItemType == ItemType.Coin & CoinSpawn)
        {
            Debug.Log("Gold Coming");
            ParticleExtension.PlayCoinEffect(pos, amount);
            Datas.Coin.CoinAdd(amount);
            return;
        }

        var spawn = GameObject.Instantiate(Item.Prefab, pos, Quaternion.identity);
        spawn.transform.DOJump(pos + new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)), 1, 1, 0.5f);
    }
}