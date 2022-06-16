using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterHealth : CharacterSetup
{
    [Title("After Die")] public bool OptionsDie;
    [ShowIf("OptionsDie")] public float DelayDie;
    public int MaxHealth;
    private int health;
    public int Health_ => health;
    protected override void OnStart()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        health = MaxHealth;
    }
    
    public void AddHealth(int value)
    {
        health += value;
        if (health > MaxHealth)
            health = MaxHealth;
    }
    public void HealthSystem(int damage)
    {
        Debug.Log("Bullet Hit");
        if (health <= 0) return;
        if (health > MaxHealth) health = MaxHealth;

        health -= damage;
        if (health <= 0)
        {
            _character.OnDeath?.Invoke();
            Debug.Log("You are dead");
            health = 0;
            WhenDead();
            _character._CharacterStat = CharacterStat.death;
            return;
        }

        _character.OnHit?.Invoke();
    }

    public virtual void WhenDead()
    {
        if (DelayDie > 0)
        {
            DOVirtual.DelayedCall(DelayDie, () => { gameObject.SetActive(false); });
            return;
        }

        gameObject.SetActive(false);
    }
}