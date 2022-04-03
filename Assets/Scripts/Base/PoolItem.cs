using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem<T> : MonoBehaviour
{
    public T _PoolEnum;
    protected virtual void OnDisable()
    {
        PoolManager.Instance.BackToList(this);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public void SetTag(string tag)
    {
        gameObject.tag = tag;
    }

    public void SetName(string name)
    {
        gameObject.name = name;
    }

    public void DeActive()
    {
        gameObject.SetActive(false);
    }


}
