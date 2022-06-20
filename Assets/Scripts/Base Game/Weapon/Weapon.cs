using System;
using UnityEngine;
using System.Collections;
public class Weapon : MonoBehaviour
{
    private WeaponData weaponData;
    private GameObject Bullet;
    private float FireSpeed;
    private bool Magazine;
    private int MagazineSize;
    private int CurrentMagazine;
    private int BackupMagazine;
    private float ReloadTime;

    public Action OnReload;
    public Action OnFire;

    private void Start()
    {
        Bullet = weaponData.Bullet;
        FireSpeed = weaponData.FireSpeed;
        Magazine = weaponData.Magazine;
        MagazineSize = weaponData.MagazineSize;
        CurrentMagazine = MagazineSize;
        BackupMagazine = CurrentMagazine;
        ReloadTime = weaponData.ReloadTime;
    }

    private void OnEnable()
    {
        if(weaponData.AutoFire)
            StartCoroutine(AutoFire());
    }

    public IEnumerator AutoFire()
    {
        while (Base.IsPlaying())
        {
            Fire();
            yield return new WaitForSeconds(FireSpeed);
        }
    }
    
    
    public void Fire()
    {
        if (CurrentMagazine > 0)
        {
            CurrentMagazine--;
            Instantiate(Bullet, transform.position, transform.rotation);
            OnFire?.Invoke();
        }
    }

    public void Reload()
    {
        if (BackupMagazine > 0)
        {
            OnReload?.Invoke();
            if (BackupMagazine > MagazineSize)
            {
                CurrentMagazine = MagazineSize;
                BackupMagazine -= MagazineSize;
            }
            else
            {
                CurrentMagazine = BackupMagazine;
                BackupMagazine = 0;
            }
        }
    }
}