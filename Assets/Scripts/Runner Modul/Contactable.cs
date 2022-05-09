using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Contactable : MonoBehaviour
{
    public ContactableData Data;
    protected int value = 1;
    private Collider _collider;
    public bool MakeTrigger = true;
    public bool AfterDestory = true;
    public LayerMask DetectedMask;
    private void Awake()
    {
        Setup();
    }

    protected virtual void Setup()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = MakeTrigger;

        if (Data == null)
        {
            Debug.LogWarning("Data is null");
            return;
        }

        var prefab = Instantiate(Data.Prefab, transform);
        prefab.transform.localPosition = Data.Pos;
        prefab.transform.localRotation = Data.Rot;
        prefab.transform.localScale = Data.Scale;
        value = Data.Value;
    }

    protected abstract void Contant(GameObject _gObject);

    private void OnTriggerEnter(Collider other)
    {

        if (ExtensionMethods.CheckLayer(other.gameObject, DetectedMask))
        {
            if (!MakeTrigger | !Base.IsPlaying()) return;

            Contant(other.gameObject);
            if (Data.AudioEffect != Enum_Audio.Empty)
                Data.AudioEffect.Play();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (ExtensionMethods.CheckLayer(other.gameObject, DetectedMask))
        {
            if (MakeTrigger | !Base.IsPlaying()) return;

            Contant(other.gameObject);
            if (Data.AudioEffect != Enum_Audio.Empty)
                Data.AudioEffect.Play();
        }
    }

}
