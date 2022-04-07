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
        if (Data == null)
        {
            Debug.LogError("Data is null");
            return;
        }

        _collider = GetComponent<Collider>();

        _collider.isTrigger = MakeTrigger;

        var prefab = Instantiate(Data.Prefab, transform);
        prefab.transform.localPosition = Data.Pos;
        prefab.transform.localRotation = Data.Rot;
        prefab.transform.localScale = Data.Scale;
        value = Data.Value;
    }

    protected abstract void Contant(GameObject _gObject);

    private void OnTriggerEnter(Collider other)
    {
        if ((DetectedMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if (!MakeTrigger | !Base.IsPlaying()) return;

            Contant(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((DetectedMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if (MakeTrigger | !Base.IsPlaying()) return;

            Contant(other.gameObject);
        }
    }

}
