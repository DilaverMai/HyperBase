using System;
using UnityEngine;

public abstract class Damage : MonoBehaviour
{
    public Action OnHit;
    public int DamageValue;
    public LayerMask ContactLayer;
    public Enum_PoolParticle Effect;
    public Enum_Audio Audio;
    protected virtual void Contact(CharacterHealth health)
    {
        Effect.GetParticle().SetPosition(transform.position);
        //Audio.Play();
        health.HealthSystem(DamageValue);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CheckLayer(ContactLayer)) return;

        if (other.TryGetComponent<CharacterHealth>(out var health))
        {
            if (health.Health_ <= 0)
                return;
            OnHit?.Invoke();
            Contact(health);
        }
    }
}