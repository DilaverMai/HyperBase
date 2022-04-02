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

    private void Awake() {
        Cameras.cam1.GetVirtualCamera().SetCamera(transform);
        Cameras.cam1.GetVirtualCamera().SetOffset(new Vector3(0, 3.5f, -10));
    }

    private void Start() {
        health = maxHealth;
    }

    private void OnEnable() {
        TakeDamage += HealthSystem;
    }

    private void OnDisable() {
        TakeDamage -= HealthSystem;
    }

    private void HealthSystem(int damage) {
        if (health <= 0) return;
        if(health > maxHealth) health = maxHealth;
        
        health -= damage;

        if (health <= 0) {
            health = 0;
            Debug.Log("You are dead");
            return;
        }
    }
}
