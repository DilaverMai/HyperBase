using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean;
using Lean.Touch;

public abstract class Weapon : MonoBehaviour
{
    public Transform Barrel;
    public WeaponData data;
    protected float _timeToNextShot;
    protected int _sameTimeBullet;
    protected float _bulletAngle;
    protected float _bulletSpeed;
    protected Enum_PoolObject bullet;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _timeToNextShot = data.timeToNextShot;
        _sameTimeBullet = data.sameTimeBullet;
        _bulletAngle = data.bulletAngle;
        _bulletSpeed = data.bulletSpeed;
        bullet = data.bullet;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {

    }

    private void OnFingerDown(LeanFinger finger)
    {
        if (finger.IsOverGui)
        {
            return;
        }

        if (Time.time > _timeToNextShot)
        {
            StartCoroutine("Shoot");
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            for (int i = 0; i < _sameTimeBullet; i++)
            {
                bullet.GetObject().SetRotation(WeaponExtension.
                AngleBullet(Barrel.position, _bulletAngle));
            }

            yield return new WaitForSeconds(_timeToNextShot);
        }
    }
}

public static class WeaponExtension
{
    public static Vector3 AngleBullet(Vector3 vector, float angle)
    {
        float x = vector.x;
        float y = vector.y;
        float z = vector.z;

        float rad = angle * Mathf.Deg2Rad;

        float newX = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
        float newY = x * Mathf.Sin(rad) + y * Mathf.Cos(rad);

        return new Vector3(newX, newY, z);
    }
}