using UnityEngine;

public abstract class Damage : MonoBehaviour
{
    public int DamageValue;
    public LayerMask ContactLayer;

    protected virtual void Contact(CharacterHealth health)
    {
        health.HealthSystem(DamageValue);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CheckLayer(ContactLayer)) return;

        if (other.TryGetComponent<CharacterHealth>(out var health))
        {
            if (health.Health_ <= 0)
                return;
            Contact(health);
        }
    }
}