using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class WeaponData : ScriptableObject
{
    public GameObject Bullet;
    public float FireSpeed;
    public bool Magazine;
    [ShowIf("Magazine")] public int MagazineSize;
    [ShowIf("Magazine")] public int CurrentMagazine;
    [ShowIf("Magazine")] public int BackupMagazine;
    [ShowIf("Magazine")] public float ReloadTime;
    public bool AutoFire;
}