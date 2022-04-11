using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraManager;
using static Enums;

public class Player : MonoBehaviour
{
    public static Action<int> TakeDamage;
    private int health;
    public int maxHealth;
    private int power;
    public int Power => power;
    internal static Player Instance;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cameras.cam1.GetVirtualCamera().SetCamera(transform);
        Cameras.cam1.GetVirtualCamera().SetOffset(new Vector3(0, 3.5f, -10));
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        TakeDamage += HealthSystem;
    }

    private void OnDisable()
    {
        Instance = null;
        TakeDamage -= HealthSystem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Enum_PoolObject.Test_Obstacle.GetObject().
            SetPosition(new Vector3(0, 5, 0));
        }
    }

    public void HealthSystem(int damage)
    {
        if (health <= 0) return;
        if (health > maxHealth) health = maxHealth;

        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Debug.Log("You are dead");
            Base.FinisGame(Enums.GameStat.Lose, 0);
            return;
        }
    }

    public void _AddPower(int _add)
    {
        power += _add;
    }

}

public static class PlayerExtension
{
    public static void AddPower(this Player player, int power)
    {
        player = Player.Instance;
        player._AddPower(power);
    }

    public static void ResetPos(this Player player)
    {
        Player.Instance.GetComponent<PlayerController>().ResetPos();
    }

    public static void HealthSystem(this Player player, int damage)
    {
        player = Player.Instance;
        player.HealthSystem(damage);
    }
}
