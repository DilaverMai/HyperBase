using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "HyperBase/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Enum_PoolObject DefualtBullet;
    public float bulletSpeed;
    public float bulletAngle;
    public float timeToNextShot;
    public int sameTimeBullet;
}
