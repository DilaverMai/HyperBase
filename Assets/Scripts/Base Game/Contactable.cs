using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Contactable : MonoBehaviour
{
    [Title("Generaly")]
    public int Value = 1;
    public bool MakeTrigger = true;
    public bool AfterDestory = true;
    public Enum_Audio Audio;
    public Enum_PoolParticle Particle;
    public LayerMask DetectedMask;
    private Collider _collider;

    private void Awake()
    {
        Setup();
    }

    protected virtual void Setup()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = MakeTrigger;
    }

    protected abstract void Contant(GameObject _gObject);

    private void OnTriggerEnter(Collider other)
    {

        if (ExtensionMethods.CheckLayer(other.gameObject, DetectedMask))
        {
            if (!MakeTrigger | !Base.IsPlaying()) return;

            Contant(other.gameObject);
            if (Audio != Enum_Audio.Empty)
                Audio.Play();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (ExtensionMethods.CheckLayer(other.gameObject, DetectedMask))
        {
            if (MakeTrigger | !Base.IsPlaying()) return;

            Contant(other.gameObject);
            if (Audio != Enum_Audio.Empty)
                Audio.Play();
        }
    }

}
