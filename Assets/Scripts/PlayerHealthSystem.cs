using System;
using UnityEngine;

public class PlayerHealthSystem :MonoBehaviour, IPlayerHealthSystem
{
    public int Health;
    public int MaxHealth;
    
    private Character baseCharacter;

    private void Awake()
    {
        baseCharacter = GetComponent<Character>();
    }

    private void OnEnable()
    {
        Health = MaxHealth;
        baseCharacter.OnDeath += OnDeath;
    }
    
    private void OnDisable()
    {
        baseCharacter.OnDeath -= OnDeath;
    }

    public bool TakeDamage(int damage)
    {
        if(Health == 0)
            return true;
        
        Health += damage;
        
        if (Health > MaxHealth)
            Health = MaxHealth;

        if (Health <= 0)
        {
            baseCharacter.OnDeath?.Invoke();
            return true;
        }

        return false;
    }


    public bool IsDead()
    {
        return Health <= 0;
    }

    public void OnDeath()
    {
        
    }
}