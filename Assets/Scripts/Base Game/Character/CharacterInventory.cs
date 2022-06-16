using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class CharacterInventory : MonoBehaviour
{
    [Title("Player Equipment")]
    public bool HaveHelmet;
    [ShowIf("HaveHelmet")] public Item Helmet;
    [ShowIf("HaveHelmet")] public Transform SpawnPointForHelmet;

    public bool HaveArmor;
    [ShowIf("HaveArmor")] public Item Armor;
    [ShowIf("HaveArmor")] public Transform SpawnPointForArmor;
    public bool HaveBoots;
    [ShowIf("HaveBoots")] public Item Boots;
    [ShowIf("HaveBoots")] public Transform SpawnPointForBoots;

    public bool HaveWeapon;
    [ShowIf("HaveWeapon")] public Item Weapon;
    [ShowIf("HaveWeapon")] public Transform SpawnPointForWeapon;

    public bool HaveShield;
    [ShowIf("HaveShield")] public Item Shield;
    [ShowIf("HaveShield")] public Transform SpawnPointForShield;

    public bool HaveRing;
    [ShowIf("HaveRing")] public Item Ring;
    [ShowIf("HaveRing")] public Transform SpawnPointForRing;

    [Title("Inventory")]
    public int MaxSize;
    public List<Item> items = new List<Item>();
    public bool AutoEquip;
    
    private CharacterLevel _characterLevel;

    private void Awake()
    {
        _characterLevel = GetComponent<CharacterLevel>();
    }

    private void OnEnable()
    {
        if (AutoEquip)
            _characterLevel.OnLevelUp += AutoEquipFunc;
    }

    private void OnDisable()
    {
        if (AutoEquip)
            _characterLevel.OnLevelUp -= AutoEquipFunc;
    }

    public Item GetItemLocationWithType(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                return Weapon;
            case ItemType.Armor:
                return Armor;
            case ItemType.Helmet:
                return Helmet;
            case ItemType.Boot:
                return Boots;
            case ItemType.Glove:
                break;
            case ItemType.Shield:
                return Shield;
            case ItemType.Ring:
                return Ring;
            case ItemType.Potion:
                break;
            case ItemType.Food:
                break;
            case ItemType.Scroll:
                break;
            case ItemType.Book:
                break;
            case ItemType.Key:
                break;
            case ItemType.None:
                break;
            default:
                return null;
        }

        return null;
    }

    protected void AutoEquipFunc()
    {
        foreach (var item in items)
        {
            if (item.ItemType == ItemType.Weapon)
            {
                if (Weapon.ItemPower < item.ItemPower)
                {
                    Weapon = item;
                }
            }
            else if (item.ItemType == ItemType.Armor)
            {
                if (Armor.ItemPower < item.ItemPower)
                {
                    Armor = item;
                }
            }
        }
    }

    private void SpawnItems()
    {
        if (HaveArmor)
        {
            if (Armor == null) return;
            var armor = Instantiate(Armor.Prefab, SpawnPointForArmor.position, Quaternion.identity);
            armor.transform.SetParent(SpawnPointForArmor);
        }

        if (HaveWeapon)
        {
            if (Weapon == null) return;
            var weapon = Instantiate(Weapon.Prefab, SpawnPointForWeapon.position, Quaternion.identity);
            weapon.transform.SetParent(SpawnPointForWeapon);
        }


        // foreach (var item in Inventory.items)
        // {
        //     var spawnedItem = Instantiate(item.Prefab, SpawnPoint);
        //     spawnedItem.transform.position = Vector3.zero;
        //     spawnedItem.transform.rotation = Quaternion.identity;
        //     //spawnedItem.gameObject.SetActive(false);
        // }
    }
}

